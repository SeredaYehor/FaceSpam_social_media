$(document).ready(function () {
    $(document).on("click", ".CommentRemove", function () {
        var id = $(this).attr("id");
        hubConnection.invoke("Delete", id);
        $.ajax({
            type: "POST",
            url: '/Home/RemoveComment',
            data: { commentId: id },
            success: function (status) {
                if (status == -1) {
                    alert("Error removing comment");
                }
                hubConnection.invoke("Remove", status);
            }
        });
    })
});

function DeleteComment(messageId) {
    var elem = document.getElementById("Comment_" + messageId);
    elem.remove();
}

function GetMessage(messageId, message, time, user, image) {
    messageObject =
        '<div id="Comment_' + messageId + '">' +
        '<div style="display: flex; align-items: center; flex-direction: row">' +
        '<img src="' + image + '" class="UserImage" />' +
        '<label class="UserName">' + user + '</label>' +
        '<label class="Time">' + time + '</label>' +
        '<img src="../images/removeButton.png" class="CommentRemove" id="'
        + messageId + '" />' +
        '</div>' +
        '<div>' +
        '<label class="PostedMessage">' + message + '</label>' +
        '</div>' +
        '</div>';
    $("#comments").append(messageObject);
}

function GetMessageWithoutRemove(messageId, message, time, user, image) {
    messageObject =
        '<div id="Comment_' + messageId + '">' +
        '<div style="display: flex; align-items: center; flex-direction: row">' +
        '<img src="' + image + '" class="UserImage" />' +
        '<label class="UserName">' + user + '</label>' +
        '<label class="Time">' + time + '</label>' +
        '</div>' +
        '<div>' +
        '<label class="PostedMessage">' + message + '</label>' +
        '</div>' +
        '</div>';
    $("#comments").append(messageObject);
}

    function AddMessage() {
        var text = document.getElementById("message").value;
        document.getElementById("message").value = null;
        var time = GetTime();

        var userName = "";
        var image = "";
        var messageId;

        $.ajax({
            url: "/Home/GetUser",
            data: { executorId: executorId },
        }).done(function (user) {
            userName = user["name"].toString();
            image = user["imageReference"].toString();
            $.ajax({
                type: "POST",
                url: "/Home/AddComment",
                async: false,
                data: { message: text, executorId: executorId, postId: postId },
                success: function (id) {
                    messageId = id;
                }
            });
            hubConnection.invoke("Send", messageId, userName, image, text, Number(executorId));
        });
    }

    function GetTime() {
        var date = new Date();

        var hours = date.getHours() < 10 ? "0" + date.getHours() : date.getHours();
        var minutes = date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes(); 
        var day = date.getDate() < 10 ? "0" + date.getDate() : date.getDate(); 
        var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
        var year = date.getFullYear().toString().substr(-2);

        return day + '.' + month + '.' + year + ' ' + hours + ':' + minutes;
    }
