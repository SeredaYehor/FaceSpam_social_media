$(document).ready(function () {
<<<<<<< HEAD
});
=======
>>>>>>> a0613f2ba4ac44925bec9e3efc1caacfcaa2ab7f

    function GetMessage(message, time) {
        messageObject =
            '<div style="display: flex; align-items: center; flex-direction: row">' +
<<<<<<< HEAD
            '<img src="../Images/W1ld 3lf.jpg" class="UserImage" />' +
=======
            '<img src="../Images/W1ld3lf.png" class="UserImage" />' +
>>>>>>> a0613f2ba4ac44925bec9e3efc1caacfcaa2ab7f
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
<<<<<<< HEAD
            data: { message: text, },
=======
            data: { message: text,},
>>>>>>> a0613f2ba4ac44925bec9e3efc1caacfcaa2ab7f
        });

        document.getElementById("message").value = null;

        var time = GetDateTime();
        GetMessage(text, time);
    }

    function GetDateTime() {
        var date = new Date().toLocaleDateString().substr(0, 6) + new Date().getFullYear().toString().substr(-2);
        var time = new Date().toLocaleTimeString().substr(0, 5);

        return date + ' ' + time;
<<<<<<< HEAD
    }
=======
    }
});
>>>>>>> a0613f2ba4ac44925bec9e3efc1caacfcaa2ab7f
