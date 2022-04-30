﻿$(document).ready(function () {

    var formData = new FormData();

    $("#Browse").click(function () {
        document.getElementById('myFile').click();
    })

    $(".DashboardList").on("click", ".HeartImage", function ChangeColor() {
        if ($(this).attr("src") == "/images/heartRed.svg") {
            $(this).attr("src", "/images/heart.svg");
        }
        else {
            $(this).attr("src", "/images/heartRed.svg");
        }
        var id = $(this).attr("id");
        var likes = UpdateLikes(id);
        $(this).parent().children(".LikeLabel").html(likes);
    });

    $("#Write").click(function () {
        var text = $(".Post").val();
        formData.set('text', text);
        $(".Post").val("");
        $.ajax({
            type: "POST",
            url: '/Home/AddPost',
            data: formData,
            contentType: false,
            processData: false,
            success: function (user) {
                AddNewPost(user["item1"], text, user["item2"], formData.get('file'));
            }
        });
    })

    function GetCurrentDate() {
        var dt = new Date();
        var date = dt.getDate() + "." + ("0" + (dt.getMonth() + 1)).slice(-2) + "." + dt.getFullYear().toString().substr(2, 2);
        return date;
    }

    function AddNewPost(user, text, postId, postImage = null) {
        var date = GetCurrentDate();
        var postObject = '<div class="PostMessage">' +
            '<img src="' + user["imageReference"] + '"class="PostEllipse" />' +
            '<label style="position: relative; font-size: 18px; color: #3485FF;">' + user["name"] + '</label>' +
            '<br />' +
            '<label class="PostText">' + text + '</label>';
            if(postImage != null)
            {
                postObject += '<div><img src="../Images/' + postImage.name + '"style = "width: 90%; margin-left: 15px;" /></div >';
            }
        postObject += '<div style="background-color:transparent; height:50px;">' +
            '<div class="Likes" style="display: inline-block;">' +
            '<img src="../images/heart.svg" class="HeartImage" id="' + postId + '" />' +
            '<label class="LikeLabel">0</label>' +
            '</div><img src="../images/pencil.svg" style="display: inline-block; margin-left: 10px;" />' +
            '<label style="position: relative; display: inline-block; font-size: 12px; color: #3485FF;">comments 0</label>' +
            '<label class="Date">' + date + '</label></div></div>';
        $(".DashboardList").append(postObject);
    }

    $("#myFile").change(function () {
        var path = document.getElementById("myFile").files[0];
        formData.set('file', path);
    })

    function UpdateLikes(id) {
        var result = 0;
        $.ajax({
            type: "GET",
            url: '/Home/ChangeLike',
            data: { postId: id, },
            async: false,
            success: function (likes) {
                result = likes;
            }
        });
        return result;
    }
});