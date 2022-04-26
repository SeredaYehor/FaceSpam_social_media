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

    $("#Browse").click(function () {
        document.getElementById('myFile').click();
    })

    $("#myFile").change(function () {
        var path = document.getElementById("myFile").files[0];
        var tb_text = $(".Post").val();
        $(".Post").val("file::" + URL.createObjectURL(path) + ";\n" + tb_text);
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