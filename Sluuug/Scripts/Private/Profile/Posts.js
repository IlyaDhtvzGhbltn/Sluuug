function CreateNewPost() {
    var title = $('#new-post-title').val();
    var text = $('#new-post-text').val();
    $.ajax({
        type : 'post',
        url: '/api/create_post',
        data: { Title: title, Text: text },
        success: function (resp) {
            console.log(resp);
            if (resp == 'False') {
                _Alert('Произошла ошибка, пожалуйста попробуйте позже.', 'red');
            }
            else {
                $('#new-post-text').val('');
                $('#new-post-title').val('');
                let newPost = PostNode.ItemUserPost(title, text);
                let postsList = $('.all-user-posts')[0];
                postsList.insertBefore(newPost, postsList.firstChild);
            }
        }
    });
}

function CheckActiveElement(elementInputId, elementEnabledIdMassive, elementDisabledIdMassive) {
    var titleLength = $('#' + elementInputId).val().length;
    if (titleLength > 0) {
        for (var i = 0; i < elementEnabledIdMassive.length; i++) {
            $('#' + elementEnabledIdMassive[i]).attr("disabled", false);
        }
    }
    else {
        for (var i = 0; i < elementDisabledIdMassive.length; i++) {
            $('#' + elementDisabledIdMassive[i]).attr("disabled", true);
        }    }
}

function GetMoreOwnPosts(currentPostsCount) {
    $.ajax({
        type: 'post',
        url: '/api/getmoreownposts',
        data: { currentPosts: currentPostsCount },
        success: function (resp) {
            let postsList = $('.all-user-posts')[0];

            [].forEach.call(resp.Posts, function (item) {
                let newPost = PostNode.ItemUserOldPost(item.PostTitle, item.PostText, item.PostedTime);
                postsList.insertBefore(newPost, postsList.lastChild);
            });
            var postsOnPageAfterUploading = currentPostsCount + resp.Posts.length;
            dropMorePostsButton();
            console.log('all post for now ' + postsOnPageAfterUploading);
            console.log('server posts ' + resp.TotalPostsCount);

            if (postsOnPageAfterUploading < resp.TotalPostsCount) {
                let button = PostNode.ButtonMorePost(postsOnPageAfterUploading);
                postsList.insertBefore(button, postsList.lastChild);
            }
        }
    });
}

function dropMorePostsButton() {
    $('.more-posts-container').remove();
}