$(document).ready(function() {
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
        var panel = '<div id="' + id + '" class="FriendLine" style="display: flex; align-items: center;">' +
            '<div class="ImageAndName">' +
            '<img class="UserImage" src="' + image + '" />' +
            '<label class="Names">' + name + '</label>' +
            '</div>' +
            '<div class="FriendButtons">' +
            '<input id="' + id + '" type="submit" class="GroupQuitButton" value="Quit" onclick="DeleteChat(this)" />&nbsp;' +
            '<form action="/Home/Messages?current=' + id + '" method="post">' +
            '<input type="submit" class="GroupWriteButton" value="Write" />' +
            '</form></div></div>';
        $(".GroupsPanels").append(panel);
    }
    
    $(".ConfirmButton").click(function () {
        addedMembers.push(userId);
        if ($("#name").val() == "") {
            alert("Error of creating group. Please, specify group name.");
            $(".CreateGroup").hide();
            return;
        }
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
                GenerateGroupPanel(group["id"], group["imageReference"],
                    group["chatName"]);
            }
        });
    })

    function AddUserPanel(id, name, image) {
        var panel = '<div class="UserPanel" id="' + name +'">' +
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
                    var id = list[i]["id"].toString();
                    var name = list[i]["name"].toString();
                    var image = list[i]["imageReference"].toString();
                    AddUserPanel(id, name, image);
                }
            }
        });
    });

    $(".GroupsPanels").on('click', ".GroupQuitButton", function () {
        var id = $(this).attr("id");
        $(this).parent(".FriendButtons").parent(".FriendLine").remove();
        $.ajax({
            type: "POST",
            url: '/Home/DeleteGroup',
            async: true,
            data: { groupId: id },
            success: function (status) {
            }
        });
    });

    $(".SearchBox").keyup(function () {
        var search = this.value;
        if (search.trim() == '') {
            $(".UserPanel").show();
        }
        else {
            $(".UserPanel").hide().filter('[id^=' + search + ']').show();
        }
    });
})
