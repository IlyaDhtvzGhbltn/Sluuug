function show_login(){
	$('#signup_form')[0].style.display = 'none';
	$('#repassword_form')[0].style.display = 'none';
	$('#login_form').fadeIn();
	
	$('#login_toggle').addClass('active');
    $('#reg_toggle').removeClass('active');

    var alerts = $('.error-message-form');
    [].forEach.call(alerts, function (item) {
        item.style.display = 'none';
    });
}
function show_register(){
	$('#login_form')[0].style.display = 'none';
	$('#repassword_form')[0].style.display = 'none';
	$('#signup_form').fadeIn();
	
	$('#reg_toggle').addClass('active');
    $('#login_toggle').removeClass('active');

    var alerts = $('.error-message-form');
    [].forEach.call(alerts, function (item) {
        item.style.display = 'none';
    });

}
function show_repass(){
	$('#signup_form')[0].style.display = 'none';
	$('#login_form')[0].style.display = 'none';	
	$('#repassword_form').fadeIn();
		
	$('#reg_toggle').removeClass('active');
    $('#login_toggle').removeClass('active');

    var alerts = $('.error-message-form');
    [].forEach.call(alerts, function (item) {
        item.style.display = 'none';
    });
}

async function show_feedback_form() {
    var len = $('.feed_back')[0].innerHTML.length;
    $('.feed_back').fadeIn();

    console.log(len);
    if (len <= 1) {
        var html = await getPartialView('/partial/feedback');
        $('.feed_back')[0].innerHTML = html;
    }
    else
    {
        $('.feed_back').fadeIn();
    }
}


function close_feedback_form() {
    $('.feed_back').fadeOut();
}

function scrolling(elementID, offset) {
    var target = $('#' + elementID)[0];
    var Y = target.offsetTop + offset;

    $('html, body').stop().animate({
        scrollTop: Y
    }, 1000);
}

$(document).ready(function () {
    $('a[href^="#"]').on('click', function (event) {
        var target = $(this.getAttribute('href'));
        if (target.length) {
            event.preventDefault();
            $('html, body').stop().animate({
                scrollTop: target.offset().top
            }, 1000);
        }
    }); 
});

window.onscroll = function () {
    if (window.pageYOffset > 530) {
        $('.head_navigation').fadeIn();
    }
    else {
        $('.head_navigation').fadeOut();
    }


    if (window.pageYOffset > 300) {
        $('#right_navigation')[0].style.display = "block";
        let offset = window.pageYOffset;

        let mainOffset = $('#main').offset().top;
        let functionOffset = $('#functions').offset().top - 100;
        let reg_logOffset = $('#reg_log').offset().top - 500;
        let helpOffset = $('#help').offset().top - 500;

        if (offset >= mainOffset && offset <= functionOffset) {
            main_circle.style.fill = 'rgba(162, 236, 243, 1)';
        }
        else {
            main_circle.style.fill = 'transparent';
        }

        if (offset >= functionOffset && offset <= reg_logOffset) {
            function_circle.style.fill = 'rgba(162, 236, 243, 1)';
        }
        else {
            function_circle.style.fill = 'transparent';
        }

        if (offset >= reg_logOffset && offset <= helpOffset) {
            reg_log_circle.style.fill = 'rgba(162, 236, 243, 1)';
        }
        else {
            reg_log_circle.style.fill = 'transparent';
        }

        if (offset >= helpOffset) {
            help_circle.style.fill = 'rgba(162, 236, 243, 1)';
        }
        else {
            help_circle.style.fill = 'transparent';
        }
    }
    else {
        $('#right_navigation')[0].style.display = "none";
    }
}