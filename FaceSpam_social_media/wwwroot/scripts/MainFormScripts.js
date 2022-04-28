$(document).ready(function () {

    var formData = new FormData();

    $(".HeartImage").click(function ChangeColor() {
        if ($(this).attr("src") == "/images/heartRed.svg") {
            $(this).attr("src", "/images/heart.svg");
        }
        else {
            $(this).attr("src", "/images/heartRed.svg");
        }
        var id = $(this).attr("id");
        var likes = UpdateLikes(id);
        $(this).parent().children(".LikeLabel").html(likes);
    })

    $("#Browse").click(function () {
        document.getElementById('myFile').click();
    })

    $("#Write").click(function () {
        var text = $(".Post").val();
        formData.set('text', text);
        $.ajax({
            type: "POST",
            url: '/Home/AddPost',
            data: formData,
            contentType: false,
            processData: false,
            success: function (user) {
                AddNewPost(user, text, formData.get('file'));
            }
        });
    })

    function AddNewPost(user, text, postImage = null) {
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
            '<img src="../images/heart.svg" class="HeartImage" id="@post.PostId" />' +
            '<label class="LikeLabel">0</label>' +
            '</div><img src="../images/pencil.svg" style="display: inline-block; margin-left: 10px;" />' +
            '<label style="position: relative; display: inline-block; font-size: 12px; color: #3485FF;">comments 0</label>' +
            '<label class="Date">' + 'choto' + '</label></div></div>';
        $(".DashboardList").prepend(postObject);
    }

    $("#myFile").change(function () {
        var path = document.getElementById("myFile").files[0];
        var tb_text = $(".Post").val();
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