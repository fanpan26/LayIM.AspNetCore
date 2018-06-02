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
                message.RoomId = RoomIdGenerator.RoomId(message.From, message.To,message.Type);
                return chatRecordRepository.Add(message);
            }
            return Task.FromResult(0);
        }

        /// <summary>
        /// 获取聊天历史记录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="targetId"></param>
        /// <param name="type"></param>
        /// <param name="fromTimestamp"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ChatMessageViewModel>> GetChatMessages(string userId, string targetId, string type, long fromTimestamp, int page = 20)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(targetId))
            {
                return LayIMNoData.NoChatMessages;
            }
            if (page <= 0) { page = 1; }

            string roomId = RoomIdGenerator.RoomId(userId, targetId, type);
            var chatRecords = await chatRecordRepository.GetList(roomId, page, fromTimestamp);
            if (chatRecords?.Count() > 0)
            {
                var userIds = chatRecords.Select(x => x.FromId);
                var users = await userRepository.GetUsersByIds(userIds);
                var result = new List<ChatMessageViewModel>();
                long uidLong = long.Parse(userId);
                foreach (var chat in chatRecords)
                {
                    var user = users.FirstOrDefault(x => x.id == chat.FromId);
                    result.Add(new ChatMessageViewModel
                    {
                        addtime = chat.AddTime,
                        from = chat.FromId,
                        msg = chat.Msg,
                        self = chat.FromId == uidLong,
                        avatar = user?.avatar,
                        username = user?.username
                    });
                }
                return result.OrderBy(x=>x.addtime);
            }
            return LayIMNoData.NoChatMessages;
        }

        /// <summary>
        /// 根据群ID获取群员列表
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public async Task<LayIMGroupMemberModel> GetGroupMembers(string userId, string groupId)
        {
            var mine = await userRepository.GetUserById(userId);
            var memberIds = await groupMemberRepository.GetUsersByGroupId(groupId);
            var members = await userRepository.GetUsersByIds(memberIds);

            LayIMGroupMemberModel result = new LayIMGroupMemberModel
            {
                list = members,
                owner = mine,
                members = members.Count()
            };
            return result;
        }
    }
}
