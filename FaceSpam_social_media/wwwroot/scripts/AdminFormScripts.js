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
        type: "POST",
        url: "/Home/UpdateUserStatus",
        async: false,
        data: { userId: user, },
        success: function (data) {
            if (obj.value == "Ban") {
                obj.value = "Unban";
            }
            else {
                obj.value = "Ban";
            }
        }
    });
}