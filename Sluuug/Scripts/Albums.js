function CreateAlbum() {
    var albumTitle = $('#album-title')[0].value;
    if (albumTitle !== '') {
        var albumDescription = $('#album-description')[0].value;
        var albumCover = $("#album-label-input")[0].files[0];

        var form = new FormData();
        form.append('album_label', albumCover);
        form.append('AlbumTitle', albumTitle);
        form.append('AlbumDescription', albumDescription);
        $.ajax({
            type: 'post',
            url: '/api/create_album',
            processData: false,
            contentType: false,
            data: form,
            success: function (resp) {
                if (resp) {
                    window.location.reload();
                }
            }
        });
    }
    else {
        $('#tooltip-container-album')[0].setAttribute('tooltip', 'Необходимо название');
        $('#album-title')[0].style.border = "1px solid #ff5151";
        setTimeout(function () {
            $('#album-title')[0].style.border = "1px solid #7f7f7f";
            $('#tooltip-container-album')[0].removeAttribute('tooltip');
        }, 5000)
    }
}


function DeleteAlbum(albumID) {
    $.ajax({
        type: "post",
        url: "/api/drop_album",
        data: { albumID },
        success: function (resp) {
            console.log(resp);
            if (resp.isSuccess) {
                document.location.reload();
            }
        }
    });
}


function ExpandAlbum(albumId) {
    console.log(albumId);
    $('.expand-foto').css('display', 'block');

    $.ajax({
        type : "post",
        url: "/api/fotos", 
        data: { album: albumId },
        success: function (resp) {
            console.log(resp);
            $('.album-images-browse')[0].innerHTML = '';
            $('.users-comments')[0].innerHTML = '';

            if (resp.PhotosCount === 0) {
                $('.image-comments').hide();
                $('.image-view').hide();
                $('.empty-album').show();
            }
            else {
                $('.image-comments').show();
                $('.image-view').show();
                $('.empty-album').hide();

                $('.image-title h3')[0].innerHTML = resp.Photos[0].Title;
                $('.image-description span')[0].innerHTML = resp.Photos[0].PhotoDescription;
                $('.full-image-container')[0].id = resp.Photos[0].ID;
                $('.full-image-container img')[0].src = resp.Photos[0].FullFotoUri;
                $('.photo-manage')[0].id = resp.Photos[0].ID;
                $('.download-photo-link')[0].href = resp.Photos[0].DownloadFotoUri;
                $('.delete-photo-button')[0].id = resp.Photos[0].ID;

                $('.album-images-browse').append('<div class="small-image" id="' + resp.Photos[0].ID + '" full_url="' + resp.Photos[0].FullFotoUri +'" onclick="ExpandPhoto(this)">' +
                    '<input type="radio" name="select-img" class="image-select-checkbox" checked>' +
                    '<img src="' + resp.Photos[0].SmallFotoUri + '">' +
                    '</div>');
                if (resp.PhotosCount > 1) {
                    for (var i = 1; i < resp.PhotosCount; i++) {
                        $('.album-images-browse').append('<div class="small-image" id="' + resp.Photos[i].ID + '" full_url="' + resp.Photos[i].FullFotoUri +'" onclick="ExpandPhoto(this)">' +
                            '<input type="radio" name="select-img" class="image-select-checkbox">' +
                            '<img src="' + resp.Photos[i].SmallFotoUri + '">' +
                            '</div>');
                    }
                }

                [].forEach.call(resp.Photos[0].FotoComments, function (comment) {
                    $('.users-comments').append('<div class="image-user-comment" onclick="redirectToUser(' + comment.UserPostedID + ')">' +
                        '<div class="comment-header">' +
                            '<h4>' + comment.UserName + ' ' + comment.UserSurName + '</h4>' +
                            '<span>' + comment.DateFormat + '</span>' +
                        '</div>' +
                        '<div class="comment-body">' +
                            '<img src="' + comment.UserPostedAvatarResizeUri + '"/>' +
                            '<span>' + comment.Text + '</span>' +
                        '</div>' +
                        '</div > ');
                });
            }
        }
    }); 
}


function ExpandPhoto(targetPhoto) {
    let fullSizeURL = targetPhoto.getAttribute('full_url');
    let photoId = targetPhoto.getAttribute('id');
    $('.full-image-container img')[0].src = fullSizeURL;
    $('.full-image-container')[0].id = photoId;
    $('.photo-manage')[0].id = photoId;
    $('.users-comments')[0].innerHTML = '';
    $('.show-photo-manage-menu')[0].checked = false;

    $.ajax({
        type: "post",
        url: "/api/get_photo_expand",
        data: { fotoId: photoId },
        success: function (resp) {
            console.log(resp);
            if (resp.isSuccess) {

                $('#photo-h3-titlte')[0].innerHTML = resp.PhotoTitle;
                $('#photo-span-description')[0].innerHTML = resp.PhotoDescription;
                $('.download-photo-link')[0].href = resp.PhotoDownloadLink;
                $('.delete-photo-button')[0].id = resp.PhotoID;


                [].forEach.call(resp.FotoComments, function (comment) {
                    $('.users-comments').append('<div class="image-user-comment" onclick="redirectToUser(' + comment.UserPostedID + ')">' +
                        '<div class="comment-header">' +
                        '<h4>' + comment.UserName + ' ' + comment.UserSurName + '</h4>' +
                        '<span>' + comment.DateFormat + '</span>' +
                        '</div>' +
                        '<div class="comment-body">' +
                        '<img src="' + comment.UserPostedAvatarResizeUri + '"/>' +
                        '<span>' + comment.Text + '</span>' +
                        '</div>' +
                        '</div >');
                });
            }
        }
    });
}


function SendPhotoComment() {
    var photoId = $('.full-image-container')[0].id;
    var commentText = $('.area-to-comment').val();
    console.log(commentText.length);
    if (commentText.length > 0) {
        $.ajax({
            type: "post",
            url: "/api/post_comments",
            data: { FotoID: photoId, CommentText: commentText },
            success: function (resp) {
                console.log(resp);
                if (resp.isSuccess) {
                    $('.users-comments').append("<div class='image-user-comment' onclick='redirectToMe()'>" +
                        '<div class="comment-header">' +
                        '<h4>Я</h4>' +
                        '<span>только что</span>' +
                        '</div>' +
                        '<div class="comment-body">' +
                        '<img src="' + $('#img_avatar')[0].src + '" style="height:55px"/>' +
                        '<span>' + commentText + '</span>' +
                        '</div>' +
                        '</div>'
                    );
                    $('.area-to-comment').val('');
                }
            }
        });
    }
}


function ChangePhotoParameter(parameter, newValue) {
    var photoId = $('.full-image-container')[0].id;
    $('.show-photo-manage-menu')[0].checked = false;
    $.ajax({
        type: "post",
        url: "/api/edit_photo",
        data: { PhotoGUID: photoId, NewValue: newValue, EditMode: parameter },
        success: function (resp) {

            if (resp.isSuccess) {
                switch (parameter)
                {
                    case 0:
                        $('#photo-h3-titlte')[0].innerHTML = newValue;
                        $('.new-photo-title').val('');
                        break;
                    case 1:
                        $('#photo-span-description')[0].innerHTML = newValue;
                        $('.new-photo-description').val('');
                        break;
                }
            }
        }
    });
}


function CloseExpandedAlbum() {
    $('.expand-foto').css('display', 'none');
    $('.show-photo-manage-menu')[0].checked = false;
}


function UploadFotosToAlbum(inputUploaded) {
    if (inputUploaded.files.length !== 0) {
        var form = new FormData();
        [].forEach.call(inputUploaded.files, function (file) {
            form.append('files', file);
        });
        console.log(inputUploaded.id);
        form.append('albumId', inputUploaded.id);

        $.ajax({
            type: "post",
            url: "/api/upload_foto",
            processData: false,
            contentType: false,
            data: form,
            success: function (resp) {
                if (resp) {
                    console.log(resp);
                    window.location.reload();
                }
            }
        });
    }
}


function DeletePhoto(Id) {
    console.log(Id);
    $.ajax({
        type: "post",
        url: '/api/drop_foto',
        data: { fotoId: Id},
        success: function (resp) {
            console.log(resp);
            if (resp.isSuccess) {
                window.location.reload();
            }
        }
    });
} 

