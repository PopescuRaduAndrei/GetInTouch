using GetInTouch.DataAccess.Infrastructure;
using GetInTouch.Logic.Infrastructure;
using GetInTouch.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetInTouch.Logic.Implementation
{
    public class CommentLogic : ICommentLogic
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;

        public CommentLogic(ICommentRepository commentRepository, IPostRepository postRepository)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
        }

        public CommentModel CreateOrUpdateComment(Guid postId, Guid userId, string commentMessage)
        {
            var model = new CommentModel
            {
                Id = Guid.NewGuid(),
                Message = commentMessage,
                PostId = postId,
                SenderId = userId,
                CreatedOn = DateTime.Now
            };

            _commentRepository.Add(model);
            return model;
        }

        public int GetPostNumberComments(Guid postId)
        {
            return _postRepository.Get(postId).Comnents.ToList().Count();
        }

    }
}
