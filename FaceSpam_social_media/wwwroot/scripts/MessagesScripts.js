    $(document).ready(function () {
        $(".GroupPanel").click(function () {
            var value = $(this).children(".GroupName").attr("id"); //get value of attribute 'id'
            ChangeActiveGroup(value); //set value as current selected group
            $(".GroupPanel").attr("class", "GroupPanel inactive"); //make unselected GroupPanel not active
            $(this).attr("class", "GroupPanel active"); //make selected GroupPanel active
            $(".ChatMessages").empty(); //clear text from textbox
        });

        function ChangeActiveGroup(myId) {
            $.ajax({  //async call of controller
                type: "POST",
                url: '/Home/SetId', //url to controller
                data: { id: myId, } //controller args
            }).done(function () { //when controller is done
                GetChatMessages(); //get all messages for selected chat
            });
        }

        $(".ButtonSend").click(function () {
            var message = $(".MessageTextBox").val(); //get textbox value
            SendMessages(message);
            $(".MessageTextBox").val("");
        });

        function GetChatMessages() {
            $.ajax({
                type: "GET",
                url: '/Home/GetChatMessages',
                success: function (messages) { //get array object of Message models
                    for (var index = 0; index < messages.length; index++) { //adding all messages for this chat
                        messageObject = '<div class="Message">' +
                            '<svg class="Circle" viewBox="0 0 35 35">' +
                            '<ellipse cx="15" cy="15" rx="15" ry="15" />' +
                            '</svg>' +
                            '<label style="position: relative; font-size: 14px; color: #3485FF;">W1ld 3lf</label>' +
                            '<label style="display: inline-block; margin-left: 200px; font-size: 12px; width: 40px; color: #3485FF; text-align: right;">' + messages[index]["time"].toString() + '</label > ' +
                            '<br />' +
                            '<label style="font-size: 12px; position: relative; left: 10px; color: #3485FF; max-width: 500px; overflow: hidden; word-wrap: break-word;">' + messages[index]["text"].toString() + '</label > ' +
                            '</div>';
                        $(".ChatMessages").append(messageObject);
                    }

                }
            });
        }

        function SendMessages(message) {
            $.ajax({ //async call of controller
                type: "GET",  //request type
                url: '/Home/SendMessage', //url to controller
                data: { textboxMessage: message, } //controller argument
            }).done(function () {
            });

            messageObject = '<div class="Message">' +
                '<svg class="Circle" viewBox="0 0 35 35">' +
                '<ellipse cx="15" cy="15" rx="15" ry="15" />' +
                '</svg>' +
                '<label style="position: relative; font-size: 14px; color: #3485FF;">W1ld 3lf</label>' +
                '<label style="display: inline-block; margin-left: 200px; font-size: 12px; width: 40px; color: #3485FF; text-align: right;">12.04</label>' +
                '<br />' +
                '<label style="font-size: 12px; position: relative; left: 10px; color: #3485FF; max-width: 500px; overflow: hidden; word-wrap: break-word;">' + message + '</label > ' +
                '</div>'; //class of message

            $(".ChatMessages").append(messageObject); //add class to chat form
        }
    });