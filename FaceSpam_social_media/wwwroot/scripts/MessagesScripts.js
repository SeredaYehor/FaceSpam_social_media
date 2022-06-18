$(document).ready(function () {
    if (selectedChat == 0) {
        $(".MessageArea").hide();
        $(".GroupInfo").hide();
    }

    $(".Popup").hide();
    var Members = new Array();

    $(".QuitGroup").click(function () {
        $.ajax({
            type: "GET",
            url: '/Home/QuitGroup',
            success: function () {
                $(".MessageArea").hide();
                $(".GroupInfo").hide();
                $(".Popup").hide();
                $(".ChatMessages").empty();
                $(".GroupPanel.active").remove();
            }
        });
    })

    $(".MembersIcon").click(function () {
        $(".MembersList").empty();
        Members = new Array();
        $(".InviteButton").val("Invite");
        $.ajax({
            type: "GET",
            url: '/Home/GetChatUsers',
            data: { chatId: selectedChat },
            success: function (members) {
                for (var i = 0; i < members.length; i++) {
                    Members.push(members[i]["id"]);
                    DisplayMember(members[i]["id"], members[i]["name"], members[i]["imageReference"], "Remove");
                }
            }
        });
        $(".Popup").show();
    })

    $(".InviteButton").click(function () {
        $(".MembersList").empty();
        if ($(this).val() == "Invite") {
            $.ajax({
                type: "POST",
                data: { exceptId: userId, },
                url: '/Home/SelectUsers',
                success: function (members) {
                    for (var i = 0; i < members.length; i++) {
                        if (Members.indexOf(members[i]["id"]) < 0) {
                            DisplayMember(members[i]["id"], members[i]["name"], members[i]["imageReference"], "Add");
                        }
                    }
                }
            });
            $(this).val("Back");
        }
        else {
            Members = new Array();
            $.ajax({
                type: "GET",
                url: '/Home/GetChatUsers',
                data: { chatId: selectedChat },
                success: function (members) {
                    for (var i = 0; i < members.length; i++) {
                        Members.push(members[i]["id"]);
                        DisplayMember(members[i]["id"], members[i]["name"], members[i]["imageReference"], "Remove");
                    }
                }
            });
            $(".Popup").show();
            $(this).val("Invite");
        }
    })


    $(".MembersList").on("click", ".RemoveMember", function () {
        var id = $(this).attr("id");
        var action = $(this).val();
        if (action == "Remove") {
            $(this).parent().remove();
            $.ajax({
                type: "POST",
                url: '/Home/RemoveChatMember',
                data: { memberId: id, },
                async: false,
                success: function (counter) {
                    Members.pop(id);
                    $(".MembersCounter").text(counter);
                }
            });
        }
        else {
            $.ajax({
                type: "POST",
                url: '/Home/AddMember',
                data: { memberId: id, },
                async: false,
                success: function (counter) {
                    $(".MembersCounter").text(counter);
                    $(".Popup").hide();
                    $(".InviteButton").val("Invite");
                    $(".MembersList").empty();
                }
            });
        }
    })

    function DisplayMember(id, name, image, value) {
        var panel = '<div class="MemberPanel">' +
            '<img src="' + image + '" class="Ellipse" />' +
            '<label class="GroupName" id="' + id + '">' + name + '</label>';
        if ((userId == chatAdmin || value != "Remove") && userId != id) {
            panel += '<input type="submit" class="RemoveMember" id="' + id + '" value="' + value + '" />';
        }
        panel += '</div>';
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
            $(".Popup").hide();
            var value = $(this).children(".GroupName").attr("id"); //get value of attribute 'id'
            selectedChat = value;
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
                 '<label class="MessageNickName" >' + name + '<label class="MessageDate">' + time + '</label ></label>';
             if (jsUser["name"] == name) {
                 messageObject += '<img src="../images/removeButton.png" class="RemoveMessage" id="' + messageId + '" />';
             }
             messageObject += '<br /><label class="MessageText">' + text + '</label ></div>';
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
                        var messageId = messages["item1"][index]["id"].toString();
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
        $(".MembersCounter").text(members);
        $(".GroupStatus").show();
        chatAdmin = groupInfo["admin"];
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
