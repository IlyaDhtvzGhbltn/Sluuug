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
    $('.expand-foto').css('display', 'block');

    $.ajax({
        type : "post",
        url: "/api/fotos", 
        data: { album: albumId },
        success: function (resp) {
            $('.album-images-browse')[0].innerHTML = '';
            $('.users-comments')[0].innerHTML = '';

            if (resp.PhotosCount === 0) {
                $('.image-comments').hide();
                $('.image-view').hide();
                $('.empty-album').show();
            }
            else {
                var json = JSON.parse(JSON.stringify(resp.Photos));

                if (resp.PhotosCount >= 2) {
                    $('.navigate-album-right')[0].onclick = function () { ExpandPhoto(1, resp.PhotosCount, json) };
                }

                $('.navigate-album-left')[0].style.opacity = 0;
                $('.navigate-album-left')[0].style.cursor = 'default';
                $('.navigate-album-left')[0].removeEventListener('click', ExpandPhoto); 

                $('.image-comments').show();
                $('.image-view').show();
                $('.empty-album').hide();

                $('.image-title h3')[0].innerHTML = resp.Photos[0].Title;
                $('.image-description span')[0].innerHTML = resp.Photos[0].PhotoDescription;
                $('.full-image-container')[0].id = resp.Photos[0].ID;
                $('.full-image-container img')[0].src = resp.Photos[0].FullFotoUri;
                try {
                    $('.photo-manage')[0].id = resp.Photos[0].ID;
                } catch{ }

                $('.download-photo-link')[0].href = resp.Photos[0].DownloadFotoUri;
                $('.delete-photo-button')[0].id = resp.Photos[0].ID;

                $('.album-images-browse').append('<div class="small-image">' +
                    '<input type="radio" name="select-img" class="image-select-checkbox" id="' + resp.Photos[0].ID + '" checked>' +
                    '<img src="' + resp.Photos[0].SmallFotoUri + '">' +
                    '</div>');
                $('.small-image')[0].onclick = function () { ExpandPhoto(0, resp.PhotosCount, json) };

                if (resp.PhotosCount > 1) {

                    for (var i = 1; i < resp.PhotosCount; i++) {
                        $('.album-images-browse').append('<div class="small-image">' +
                            '<input type="radio" name="select-img" class="image-select-checkbox" id="' + resp.Photos[i].ID +'">' +
                            '<img src="' + resp.Photos[i].SmallFotoUri + '">' +
                            '</div>');
                        $('.small-image')[i].onclick = (function (i) {
                            return function () {
                                ExpandPhoto(i, resp.PhotosCount, json);
                            }
                        })(i);
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

function ExpandPhoto(currentPhotoIndex, photosCount, allPhotos) {
    console.log('current - ' + currentPhotoIndex);
    console.log('count - ' + photosCount);
    console.log(allPhotos);
    
    if (currentPhotoIndex + 1 == photosCount) {
        $('.navigate-album-right')[0].style.opacity = 0;
        $('.navigate-album-right')[0].style.cursor = 'default';
        $('.navigate-album-right')[0].removeEventListener('click', ExpandPhoto); 

        $('.navigate-album-left')[0].style.opacity = 1;
        $('.navigate-album-left')[0].style.cursor = 'pointer';
        $('.navigate-album-left')[0].onclick = function () { ExpandPhoto(currentPhotoIndex - 1, photosCount, allPhotos) }; 
    }
    if (currentPhotoIndex == 0) {
        $('.navigate-album-left')[0].style.opacity = 0;
        $('.navigate-album-left')[0].style.cursor = 'default';
        $('.navigate-album-left')[0].removeEventListener('click', ExpandPhoto); 

        $('.navigate-album-right')[0].style.opacity = 1;
        $('.navigate-album-right')[0].style.cursor = 'pointer';
        $('.navigate-album-right')[0].onclick = function () { ExpandPhoto(currentPhotoIndex + 1, photosCount, allPhotos) }; 
    }

    if (currentPhotoIndex != 0 && currentPhotoIndex + 1 != photosCount)
    {
        $('.navigate-album-left')[0].style.opacity = 1;
        $('.navigate-album-left')[0].style.cursor = 'pointer';
        $('.navigate-album-left')[0].onclick = function () { ExpandPhoto(currentPhotoIndex - 1, photosCount, allPhotos) }; 

        $('.navigate-album-right')[0].style.opacity = 1;
        $('.navigate-album-right')[0].style.cursor = 'pointer';
        $('.navigate-album-right')[0].onclick = function () { ExpandPhoto(currentPhotoIndex + 1, photosCount, allPhotos) }; 
    }

    $.ajax({
        type: "post",
        url: "/api/get_photo_expand",
        data: { fotoId: allPhotos[currentPhotoIndex].ID },
        success: function (resp) {
            if (resp.isSuccess) {
                $('.full-image-container img')[0].src = allPhotos[currentPhotoIndex].FullFotoUri;
                $('.full-image-container')[0].id = allPhotos[currentPhotoIndex].ID;
                $('.photo-manage')[0].id = allPhotos[currentPhotoIndex].ID;
                $('.users-comments')[0].innerHTML = '';
                $('.show-photo-manage-menu')[0].checked = false;
                $('#' + allPhotos[currentPhotoIndex].ID + '.image-select-checkbox')[0].checked = true;

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
        try {
            $.ajax({
                type: "post",
                url: "/api/upload_foto",
                processData: false,
                contentType: false,
                data: form,
                success: function (data, textStatus, xhr) {
                    inputUploaded.value = "";

                    var isError = xhr.getResponseHeader("Error");
                    if (isError) {
                        _Alert(data);
                    }
                }
            });
        }
        catch (ex) {
            console.log(ex);
        }
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

