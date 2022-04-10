$(document).ready(function () {
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