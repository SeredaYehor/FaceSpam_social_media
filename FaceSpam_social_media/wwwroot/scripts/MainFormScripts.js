$(document).ready(function () {
    $(".HeartImage").click(function () {
        var postId = $(this).attr("id");
        var newImg = '';
        if ($(this).attr("src") == "/images/heartRed.svg") {
            newImg = '<img src="/images/heart.svg" class="HeartImage" id=' + postId + '/>';
        }
        else {
            newImg = '<img src="/images/heartRed.svg" class="HeartImage" id=' + postId + '/>';
        }
        $(this).replaceWith(newImg);
    })
});