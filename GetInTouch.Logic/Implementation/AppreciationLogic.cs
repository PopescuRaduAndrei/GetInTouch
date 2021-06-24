using GetInTouch.DataAccess.Infrastructure;
using GetInTouch.Logic.Infrastructure;
using GetInTouch.Logic.ViewModels.Appreciation;
using GetInTouch.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetInTouch.Logic.Implementation
{
    public class AppreciationLogic : IAppreciationLogic
    {
        private Dictionary<Emoji, string> _appreciations = new Dictionary<Emoji, string>
        {
            { Emoji.Like, "👍"},
            { Emoji.Love, "❤️"},
            { Emoji.Haha, "😂" },
            { Emoji.Wow, "😮" },
            { Emoji.Sad, "😢" },
            { Emoji.Angry, "😡" }
        };

        private readonly IAppreciationRepository _appreciationRepository;
        private readonly IPostRepository _postRepository;

        public AppreciationLogic(IAppreciationRepository appreciationRepository, IPostRepository postRepository)
        {
            _appreciationRepository = appreciationRepository;
            _postRepository = postRepository;
        }

        public AppreciationModel CreateOrUpdateAppreciation(Guid postId, Guid userId, Emoji emoji)
        {
            var model = _appreciationRepository.Get(postId, userId);

            if (model == null)
            {
                model = new AppreciationModel
                {
                    Id = Guid.NewGuid(),
                    PostId = postId,
                    SenderId = userId,
                    Emoji = emoji,
                    CreatedOn = DateTime.Now
                };

                _appreciationRepository.Add(model);
            }
            else
            {
                model.Emoji = model.Emoji != emoji ? emoji : Emoji.None;
                model.CreatedOn = DateTime.Now;
            }
            
            return model;
        }

        public AppreciationViewModel GetPostAppreciationInfo(Guid postId, Guid userId)
        {
            var post = _postRepository.Get(postId);

            var viewModel = new AppreciationViewModel
            {
                AppreciationsNumber = post.Appreciations.Where(p => p.Emoji != Emoji.None).Count(),
                GivenAppreciations = GetGivenAppreciations(post),
                AppreciationButtonMessage = GetAppreciationButtonMessage(post, userId)
            };

            return viewModel;
        }

        #region Private methods

        private string GetGivenAppreciations(PostModel post)
        {
            var result = String.Empty;
            
            var appreciationsGroups = post.Appreciations.Where(p => p.Emoji != Emoji.None).Select(p => p.Emoji).GroupBy(p => p);
            foreach (var group in appreciationsGroups)
            {
                result += _appreciations.FirstOrDefault(a => a.Key == group.Key).Value;
            }

            return result;
        }

        private string GetAppreciationButtonMessage(PostModel post, Guid userId)
        {
            var result = String.Empty;
            var appreciation = post.Appreciations.FirstOrDefault(p => p.SenderId == userId && p.Emoji != Emoji.None);
            
            if (appreciation == null)
            {
                result = nameof(Emoji.Like);
            }
            else
            {
                var emoji = _appreciations.FirstOrDefault(a => a.Key == appreciation.Emoji);
                result = emoji.Value + " " + emoji.Key.ToString();
            }

            return result;
        }

        #endregion
    }
}
