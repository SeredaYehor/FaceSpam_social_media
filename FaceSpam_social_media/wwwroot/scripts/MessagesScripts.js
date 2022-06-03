$(document).ready(function () {
    if (selectedChat == 0) {
        $(".MessageArea").hide();
        $(".GroupInfo").hide();
    }
    $(".Popup").hide();

    $(".GroupStatus").click(function () {
        $(".MembersList").empty();
        $.ajax({
            type: "GET",
            url: '/Home/GetChatUsers',
            success: function (members) {
                for (var i = 0; i < members.length; i++) {
                    DisplayMember(members[i]["userId"], members[i]["name"], members[i]["imageReference"]);
                }
            }
        });
        $(".Popup").show();
    })

    function DisplayMember(id, name, image) {
        var panel = '<div class="MemberPanel">' +
            '<img src="' + image + '" class="Ellipse" />' +
            '<label class="GroupName" id="' + id + '">' + name + '</label>';
        $(".MembersList").append(panel);
    }

    $("#ClosePopup").click(function () {
        $(".Popup").hide();
    })


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
                    SetGroupPanel(messages["item2"], messages["item3"]);
                    for (var index = 0; index < messages["item1"].length; index++) { //adding all messages for this chat
                        var messageId = messages["item1"][index]["messageId"].toString();
                        var dt = new Date(messages["item1"][index]["dateSending"]);
                        var time = dt.getHours() + ":" + dt.getMinutes() + ":" + dt.getSeconds();
                        var text = messages["item1"][index]["text"].toString();
                        var userName = messages["item1"][index]["userUser"]["name"].toString();
                        var userImg = messages["item1"][index]["userUser"]["imageReference"].toString();
                        GetMessageObj(messageId, userName, userImg, time, text);
                    }
                }
            });
        }

    function SetGroupPanel(groupInfo, members) {
        $(".GroupInfo").children(".ImageNameGroup").children(".GroupImage").attr("style",
            "background: url(\'" + groupInfo["imageReference"].toString() + "\'); background-size: cover; background-repeat: no-repeat;");
        $(".GroupPanelName").text(groupInfo["chatName"].toString());
        $(".GroupDescription").text(groupInfo["description"]);
        if (members > 2) {
            $(".MembersCounter").text(members);
            $(".GroupStatus").show();
        }
        else {
            $(".GroupStatus").hide();
        }
        $(".GroupInfo").attr("style", "");
    }

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