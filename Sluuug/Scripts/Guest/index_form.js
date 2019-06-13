function show_login(){
	$('#signup_form')[0].style.display = 'none';
	$('#repassword_form')[0].style.display = 'none';
	$('#login_form').fadeIn();
	
	$('#login_toggle').addClass('active');
    $('#reg_toggle').removeClass('active');

    $('.alert_form')[0].style.display = 'none';

}
function show_register(){
	$('#login_form')[0].style.display = 'none';
	$('#repassword_form')[0].style.display = 'none';
	$('#signup_form').fadeIn();
	
	$('#reg_toggle').addClass('active');
    $('#login_toggle').removeClass('active');

    $('.alert_form')[0].style.display = 'none';

}
function show_repass(){
	$('#signup_form')[0].style.display = 'none';
	$('#login_form')[0].style.display = 'none';	
	$('#repassword_form').fadeIn();
		
	$('#reg_toggle').removeClass('active');
    $('#login_toggle').removeClass('active');

    $('.alert_form')[0].style.display = 'none';

}

function show_feedback_form(){
	$('.feed_back_form')[0].style.display = 'block';
}
function close_feedback_form(){
	$('.feed_back_form')[0].style.display = 'none';
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
    if (window.pageYOffset > 300) {
        $('#right_navigation')[0].style.display = "block";
        let offset = window.pageYOffset;

        let mainOffset = $('#main').offset().top;
        let reg_logOffset = $('#reg_log').offset().top - 200;
        let functionOffset = $('#functions').offset().top - 100;
        let helpOffset = $('#help').offset().top - 200;

        if (offset >= mainOffset && offset <= reg_logOffset) {
            main_circle.style.fill = 'rgba(162, 236, 243, 1)';
        }
        else {
            main_circle.style.fill = 'transparent';
        }

        if (offset >= reg_logOffset && offset <= functionOffset) {
            function_circle.style.fill = 'rgba(162, 236, 243, 1)';
        }
        else {
            function_circle.style.fill = 'transparent';
        }

        if (offset >= functionOffset && offset <= helpOffset) {
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