$(document).ready(function () {
        $(".MessageArea").hide();

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

         function GetMessageObj(messageId, name, image, time, text) {
            messageObject = '<div class="MessageBody">' +
                '<img src="' + image + '" class="Ellipse" style="width: 50px; height: 50px;"/>' +
                 '<label class="MessageNickName" >' + name + '<label class="MessageDate">' + time + '</label ></label>' +
                 '<img src="../images/removeButton.png" class="RemoveMessage" id="' + messageId + '" />' +
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
                        var messageId = messages[index]["messageId"].toString();                      
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

        /*function SendMessages(message) {
            $.ajax({ //async call of controller
                type: "POST",  //request type
                url: '/Home/SendMessage', //url to controller
                data: { textboxMessage: message, }, //controller argument
<<<<<<< HEAD
                success: function (user) { //get array object of Message models
                    var name = user["item1"]["name"].toString();
                    var image = user["item1"]["imageReference"].toString();
                    var id = user["item2"].toString();
=======
                success: function (result) { //get array object of Message models
                    var name = jsUser["name"];
                    var image = jsUser["imageReference"];
>>>>>>> d6cdff163649e3b7d0f25b34a7a000f7828156a9
                    var dt = new Date();
                    var time = dt.getHours() + ":" + dt.getMinutes() + ":" + dt.getSeconds();
                    GetMessageObj(id, name, image, time, message); //class of message
                }
            });
        }*/
        function SendMessages(message) {
            $.ajax({ //async call of controller
                type: "GET",  //request type
                url: '/Home/SendMessage', //url to controller
                data: { textboxMessage: message, }, //controller argument
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