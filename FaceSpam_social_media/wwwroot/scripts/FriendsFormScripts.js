﻿$(document).ready(function () {

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

function DeleteFriend(obj, friendPage = false) {

    var user = obj.id;
    $.ajax({
        type: "GET",
        url: "/Home/DeleteFriend",
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

function AddFriend(obj) {
    var user = obj.id;

    $.ajax({
        type: "GET",
        url: "/Home/AddFriend",
        async: false,
        data: { id: user, },
    });
}

function CheckAction(obj) {
    //This switch should be changed to if statement
    switch (obj.value) {
        case "Remove":
            obj.value = "Pal up";
            DeleteFriend(obj);
            break;
        case "Pal up":
            obj.value = "Remove";
            AddFriend(obj);
            break;
    }   
}
