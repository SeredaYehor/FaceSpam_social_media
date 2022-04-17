$(document).ready(function () {

    $(".Search").keyup(function () {
        var search = this.value;
        if (search.trim() == '') {
            $(".FriendLine").show();
        }
        else {
            $(".FriendLine").hide().filter('[id^=' + search + ']').show();
        }
    });
});

function DeleteFriend(obj) {

    var user = obj.id;
    $.ajax({
        type: "GET",
        url: "/Home/DeleteFriend",
        async: false,
        data: { id: user, },
        success: function () {
            var parentDiv = $(obj).parent().parent();
            $(parentDiv).remove();
        }
    });
}