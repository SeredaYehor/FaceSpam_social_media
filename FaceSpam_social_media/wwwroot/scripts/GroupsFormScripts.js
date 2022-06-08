﻿$(document).ready(function() {
    var addedMembers = Array();
    var formData = new FormData();

    $(".Upload").click(function () {
        document.getElementById('myFile').click();
    })

    $("#myFile").change(function () {
        const path = document.getElementById("myFile").files[0];
        $(".GroupImage").empty();
        if (path) {
            formData.set('file', path);
            const fileReader = new FileReader();
            fileReader.readAsDataURL(path);
            fileReader.addEventListener("load", function () {
                var image = '<img src="' + fileReader.result + '" />';
                $(".GroupImage").append(image);
            });
        }
    })

    $(".List").on('click', ".MemberActionButton", function () {
        var action = $(this).attr("value").toString();
        var id = $(this).attr("id").toString();
        if (action == "Add") {
            addedMembers.push(id);
            action = "Rem";
        }
        else {
            addedMembers.pop(id);
            action = "Add";
        }
        $(this).attr("value", action);
    })

    function GenerateGroupPanel(id, image, name) {
        var panel = '<div id="' + id + '" class="FriendLine" style="display: flex; align-items: center; flex-direction: row">' +
            '<img class="UserImage" src="' + image + '" />' +
            '<label class="Names">' + name + '</label>' +
            '<input id="' + id + '" type="submit" class="FriendButton" value="Quit" onclick="DeleteChat(this)" />' +
            '<form action="/Home/Messages?current=' + id + '" method="post">' +
            '<input type="submit" class="GroupWriteButton" value="Write" />' +
            '</form></div>';
        $(".GroupsPanels").append(panel);
    }
    
    $(".ConfirmButton").click(function () {
        formData.set("chatName", $("#name").val());
        formData.set("chatDescription", $("#description").val());
        formData.set("members", addedMembers);
        $(".CreateGroup").hide();
        $.ajax({
            type: "POST",
            url: '/Home/CreateGroup',
            async: false,
            data: formData,
            contentType: false,
            processData: false,
            success: function (group) {
                GenerateGroupPanel(group["chatId"], group["imageReference"],
                    group["chatName"]);
            }
        });
    })

    function AddUserPanel(id, name, image) {
        var panel = '<div class="UserPanel">' +
            '<img src="' + image + '" class="UserPanelImage" />' +
            '<label>' + name + '</label>' +
            '<input type="submit" class="MemberActionButton" value="Add" id="' + id + '"/>' +
            '</div >';
        $(".List").append(panel);
    }

    $(".CreateGroupButton").click(function () {
        $(".CreateGroup").show();
        $.ajax({
            type: "POST",
            url: '/Home/SelectUsers',
            async: false,
            success: function (list) {
                for (var i = 0; i < list.length; i++) {
                    var id = list[i]["userId"].toString();
                    var name = list[i]["name"].toString();
                    var image = list[i]["imageReference"].toString();
                    AddUserPanel(id, name, image);
                }
            }
        });
    });

    $(".GroupsPanels").on('click', ".FriendButton", function () {
        var id = $(this).attr("id");
        $(this).parent(".FriendLine").remove();
        $.ajax({
            type: "POST",
            url: '/Home/QuitGroup',
            async: false,
            data: { groupId: id },
            success: function (status) {
            }
        });
    });
})