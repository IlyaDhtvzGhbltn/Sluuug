window.onload = function(){
	$('#menu-toggle').click(function(){
	var left_menu_position = $('#left-navigation').position();
        if (left_menu_position.left === 0) {
            $('#left-navigation').animate({ 'left': '-167' }, 222);
            $('.private-content').animate({ 'left': '33' }, 222);
        }
        else {
            $('#left-navigation').animate({ 'left': '0' }, 222);
            $('.private-content').animate({ 'left': '220' }, 222);

        }
});
}