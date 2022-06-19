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

function DeleteFollow(obj, friendPage = false) {

    var user = obj.id;
    $.ajax({
        type: "GET",
        url: "/Home/DeleteFollow",
        async: false,
        data: { id: user, },
        success: function () {
            if (friendPage) {
                var parentDiv = $(obj).parent();
                $(parentDiv).remove();
            }
        }
    });
}

function AddFollow(obj) {
    var user = obj.id;

    $.ajax({
        type: "GET",
        url: "/Home/AddFollow",
        async: false,
        data: { id: user, },
    });
}

function CheckAction(obj) {
    if (obj.value == "Remove") {
        obj.value = "Follow";
        DeleteFollow(obj);
    }
    else {
        obj.value = "Remove";
        AddFollow(obj);
    }
}