$(document).ready(function () {
    $(".MessageArea").hide();
    var selectedGroup = "";

        $(".ChatMessages").on("click", ".RemoveMessage", function () {
            var id = $(this).attr("id");
            $(this).parent(".MessageBody").remove();
            $.ajax({
                type: "POST",
                url: '/Home/RemoveMessage',
                data: { messageId: id, },
                success: function (status) {
                    if (status == 0) {
                        alert("Error removing message");
                    }
                }
            });
        })

    function AddToSignalRGroup(name, id) {
        $.ajax({
            type: "POST",
            url: '/Home/AddToGroup',
            async: false,
            data: { hubId: id, groupName: name},
            success: function () {
            }
        });
    }

        $(".GroupPanel").click(function () {

            $(".MessageArea").show();
            var value = $(this).children(".GroupName").attr("id"); //get value of attribute 'id'
            selectedGroup = value;
            AddToSignalRGroup(selectedGroup, connectionId);
            $(".ChatMessages").empty(); //clear text from textbox
            GetChatMessages(value); //get all messages for selected chat
            $(".GroupPanel").attr("class", "GroupPanel inactive"); //make unselected GroupPanel not active
            $(this).attr("class", "GroupPanel active"); //make selected GroupPanel active
        });

        $(".ButtonSend").click(function () {
            var message = $(".MessageTextBox").val(); //get textbox value
            SendMessages(message);
            $(".MessageTextBox").val("");
        });

        function GetChatMessages(id) {
            $.ajax({
                type: "GET",
                url: '/Home/GetChatMessages',
                async: false,
                data: { chatId: id },
                success: function (messages) { //get array object of Message models
                    for (var index = 0; index < messages.length; index++) { //adding all messages for this chat
                        var messageId = messages[index]["id"].toString();
                        var dt = new Date(messages[index]["dateSending"]);
                        var time = dt.getHours() + ":" + dt.getMinutes() + ":" + dt.getSeconds();
                        var text = messages[index]["text"].toString();
                        var userName = messages[index]["userUser"]["name"].toString();
                        var userImg = messages[index]["userUser"]["imageReference"].toString();
                        GetMessageObj(messageId, userName, userImg, time, text);
                    }
                }
            });
        }

    function SendMessages(message) {
            $.ajax({ //async call of controller
                type: "GET",  //request type
                url: '/Home/SendMessage', //url to controller
                data: { textboxMessage: message, groupName: selectedGroup, hubId: connectionId, }, //controller argument
                async: true,
                success: function (user) { //get array object of Message models
                    var name = user["item1"]["name"].toString();
                    var image = user["item1"]["imageReference"].toString();
                    var id = user["item2"].toString();
                    var dt = new Date();
                    var time = dt.getHours() + ":" + dt.getMinutes() + ":" + dt.getSeconds();
                    GetMessageObj(id, name, image, time, message); //class of message
                }
            });
        }

        $(".SearchTextBox").keyup(function () {
            var search = this.value;
            if (search.trim() == '') {
                $(".GroupPanel").show();
            }
            else {
                $(".GroupPanel").hide().filter('[id^=' + search + ']').show();
            }
        });
    });