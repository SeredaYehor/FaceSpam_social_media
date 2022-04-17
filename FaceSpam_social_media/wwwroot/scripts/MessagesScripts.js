$(document).ready(function () {
        $(".MessageArea").hide();

        $(".GroupPanel").click(function () {
            $(".MessageArea").show();
            var value = $(this).children(".GroupName").attr("id"); //get value of attribute 'id'
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

         function GetMessageObj(name, image, time, text) {
            messageObject = '<div class="MessageBody">' +
                '<img src="' + image + '" class="Ellipse" style="width: 50px; height: 50px;"/>' +
                '<label class="MessageNickName" >' + name + '<label class="MessageDate">' + time + '</label ></label>' +
                '<br /><label class="MessageText">' + text + '</label ></div>';

             $(".ChatMessages").append(messageObject);
         }

        function GetChatMessages(id) {
            $.ajax({
                type: "GET",
                url: '/Home/GetChatMessages',
                async: false,
                data: { chatId: id },
                success: function (messages) { //get array object of Message models
                    for (var index = 0; index < messages.length; index++) { //adding all messages for this chat
                        var dt = new Date(messages[index]["dateSending"]);
                        var time = dt.getHours() + ":" + dt.getMinutes() + ":" + dt.getSeconds();
                        var text = messages[index]["text"].toString();
                        var userName = messages[index]["userUser"]["name"].toString();
                        var userImg = messages[index]["userUser"]["imageReference"].toString();
                        GetMessageObj(userName, userImg, time, text);
                    }
                }
            });
        }

        function SendMessages(message) {
            $.ajax({ //async call of controller
                type: "GET",  //request type
                url: '/Home/SendMessage', //url to controller
                data: { textboxMessage: message, }, //controller argument
                success: function (user) { //get array object of Message models
                    var name = user["name"].toString();
                    var image = user["imageReference"].toString();
                    var dt = new Date();
                    var time = dt.getHours() + ":" + dt.getMinutes() + ":" + dt.getSeconds();
                    GetMessageObj(name, image, time, message); //class of message
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