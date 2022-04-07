$(document).ready(function () {
});

function GetMessage(message, time) {
    messageObject =
        '<div style="display: flex; align-items: center; flex-direction: row">' +
        '<img src="../Images/W1ld3lf.png" class="UserImage" />' +
        '<label class="UserName"> W1ld3lf </label>' +
        '<label class="Time">' + time + '</label>' +
        '</div>' +
        '<div>' +
        '<label class="PostedMessage">' + message + '</label>' +
        '</div>';
    $("#comments").append(messageObject);
}

function AddMessage() {
    var text = document.getElementById("message").value;

    $.ajax({
        type: "GET",
        url: "/Home/AddComment",
        async: false,
        data: { message: text,},
    });

    var date = new Date();
    //var time = date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds();
    var time = date.toLocaleString().replace(',', ' ');
    GetMessage(text, time);
}