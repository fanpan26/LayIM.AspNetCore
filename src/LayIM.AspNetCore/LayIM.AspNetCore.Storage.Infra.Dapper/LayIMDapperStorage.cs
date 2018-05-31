using LayIM.AspNetCore.Core.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using LayIM.AspNetCore.Core.Models;
using LayIM.AspNetCore.Storage.Infra.Dapper.Repository;
using System.Threading.Tasks;
using LayIM.AspNetCore.Core.Models.Base;
using System.Linq;
using LayIM.AspNetCore.Storage.Infra.Dapper.Models;

namespace LayIM.AspNetCore.Storage.Infra.Dapper
{
    public class LayIMDapperStorage : ILayIMStorage
    {
        #region 构造函数 私有方法
        private readonly UserRepository userRepository;
        private readonly FriendGroupRepository friendGroupRepository;
        private readonly FriendRelationRepository friendRelationRepository;
        private readonly BigGroupRepository bigGroupRepository;
        private readonly GroupMemberRepository groupMemberRepository;
        private readonly ChatRecordRepository chatRecordRepository;


        public LayIMDapperStorage(UserRepository userRepository,
            FriendGroupRepository friendGroupRepository,
            FriendRelationRepository friendRelationRepository,
            BigGroupRepository bigGroupRepository,
            GroupMemberRepository groupMemberRepository,
            ChatRecordRepository chatRecordRepository)
        {
            this.userRepository = userRepository;
            this.friendGroupRepository = friendGroupRepository;
            this.friendRelationRepository = friendRelationRepository;
            this.bigGroupRepository = bigGroupRepository;
            this.groupMemberRepository = groupMemberRepository;
            this.chatRecordRepository = chatRecordRepository;
        }

        private static readonly List<UserModel> NoUser = new List<UserModel>();
        #endregion

        /// <summary>
        /// 获取初始化数据
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<LayIMInitModel> GetInitData(string userId)
        {
            var mineTsk = userRepository.GetUserById(userId);
            var friendGroupsTask = friendGroupRepository.GetUserGroups(userId);
            var friendRelationsTask = friendRelationRepository.GetFriendRelations(userId);
            var groupIdsTask = groupMemberRepository.GetUserBigGroups(userId);

            LayIMInitModel initModel = new LayIMInitModel
            {
                //用户自己
                mine = await mineTsk
            };
            //好友列表
            List<FriendGroupModel> friend = new List<FriendGroupModel>();

            IEnumerable<FriendGroupModel> friendGroups = await friendGroupsTask;
            IEnumerable<FriendRelationShip> friendRelations = await friendRelationsTask;
            IEnumerable<long> friendIds = friendRelations.Select(x => x.FriendId);

            IEnumerable<UserModel> friends = await userRepository.GetUsersByIds(friendIds);

            if (friendIds?.Count() > 0)
            {
                foreach (var group in friendGroups)
                {
                    var friendIdsInGroup = friendRelations.Where(r => r.GroupId == group.id).Select(r => r.FriendId);

                    group.list = friends.Where(x => friendIdsInGroup.Any(f => f == x.id));
                }
            }

            friend.AddRange(friendGroups);

            initModel.friend = friend;

            //群组列表
            IEnumerable<long> groupIds = await groupIdsTask;
            var bigGroupsTask = bigGroupRepository.GetBigGroups(groupIds);
            initModel.group = await bigGroupsTask;

            return initModel;
        }

        /// <summary>
        /// 保存聊天记录【单条即时保存】
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task<int> SaveMessage(LayIMMessageModel message)
        {
            if (message?.IsVlid == true)
            {
                return chatRecordRepository.Add(message);
            }
            return Task.FromResult(0);
        }
    }
}
