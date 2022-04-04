$(document).ready(function () {
        $(".MessageArea").hide();

        $(".GroupPanel").click(function () {
            $(".MessageArea").show();
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

        function GetMessageObj(time, text) {
            messageObject = '<div class="Message"><div class="MessageBody">' +
                '<img src="../images/W1ld 3lf.jpg" class="Ellipse" style="width: 50px; height: 50px;"/>' +
                '<label class="MessageNickName" >W1ld 3lf<label class="MessageDate">' + time + '</label ></label>' +
                '<br /><label class="MessageText">' + text + '</label ></div>' +
                '</div></div>';
            $(".ChatMessages").append(messageObject);
        }

        function GetChatMessages() {
            $.ajax({
                type: "GET",
                url: '/Home/GetChatMessages',
                success: function (messages) { //get array object of Message models
                    for (var index = 0; index < messages.length; index++) { //adding all messages for this chat
                        var time = messages[index]["time"].toString();
                        var text = messages[index]["text"].toString()
                        GetMessageObj(time, text);
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
            var dt = new Date();
            var time = dt.getHours() + ":" + dt.getMinutes() + ":" + dt.getSeconds();
            GetMessageObj(time, message); //class of message
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