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

function GetMorePosts(currentPostsCount) {

}