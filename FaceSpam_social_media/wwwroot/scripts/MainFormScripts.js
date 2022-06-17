$(document).ready(function () {

    var formData = new FormData();

    $(".DashboardList").on("click", ".RemovePost", function () {
        var id = $(this).attr("id");
        $(this).parent(".PostMessage").remove();
        $.ajax({
            type: "POST",
            url: '/Home/RemovePost',
            data: { postId: id },
            success: function (status) {
                if (status == 0) {
                    alert("Error removing post");
                }
            }
        });
    })

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
            success: function (post) {
                if (post["item2"] == -1) {
                    alert("Error of adding post. You can only upload images of .jpg or .png formats");
                }
                else {
                    AddNewPost(post["item1"], text, post["item2"], formData.get('file'));
                }
                $("#Browse").text("Browse");
                formData = new FormData();
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
            '<label style="margin-left: 3px; position: relative; font-size: 18px; color: #3485FF;">' + user["name"] + '</label>' +
            '<img src="../images/removeButton.png" class="RemovePost" id="' + postId + '" />' +
            '<br />' +
            '<label class="PostText">' + text + '</label>';
            if(postImage != null)
            {
                postObject += '<div><img src="../Images/' + postImage.name + '"style = "width: 90%; margin-left: 15px;" /></div >';
            }
        postObject += '<div style="background-color:transparent; height:50px;">' +
            '<div class="Likes" style="display: inline-block;">' +
            '<form method="post" action="/Home/Comments">' +
            '<img src="../images/heart.svg" style="margin-right: 5px;" class="HeartImage" id="' + postId + '" />' +
            '<label class="LikeLabel" style="margin-right:5px;">0</label>' +
            '<img src="../images/pencil.svg" style="display: inline-block; margin-right:8px;" />' +
            '<button class="CommentButton" name="id" value="' + postId + '" type="submit">comments</button>' +
            '<label class="Date">' + date + '</label></form></div></div>';
        $(".DashboardList").prepend(postObject);
    }

    $("#myFile").change(function () {
        var path = null;
        path = document.getElementById("myFile").files[0];
        if (path != null) {
            $("#Browse").text("Browsed");
        }
        formData.set('file', path);
    })

    function UpdateLikes(id) {
        var result = 0;
        $.ajax({
            type: "GET",
            url: '/Home/ChangeLike',
            data: { postId: id },
            async: false,
            success: function (likes) {
                result = likes;
            }
        });
        return result;
    }
});
