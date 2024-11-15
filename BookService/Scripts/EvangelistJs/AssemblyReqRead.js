var PagedGridModel = function (items) {
    this.items = ko.observableArray(items);

    this.getAssemblies = function () {
        this.items.push({ name: "New item", sales: 0, price: 100 });
    };

    this.addItem = function () {
        this.items.push({ name: "New item", sales: 0, price: 100 });
    };
    this.isAscOrder= false;
    this.sortByName = function () {
        if (this.isAscOrder) {
            this.items.sort(function (a, b) {
                return b.AssemblyName < a.AssemblyName ? -1 : 1;
            });
            this.isAscOrder = false;
        }
        else {
            this.items.sort(function (a, b) {
                return a.AssemblyName < b.AssemblyName ? -1 : 1;
            });
            this.isAscOrder = true;
        }
    };

    this.jumpToFirstPage = function () {
        this.gridViewModel.currentPageIndex(0);
    };

    this.gridViewModel = new ko.simpleGrid.viewModel({
        data: this.items,
        columns: [
            { headerText: "RequestId", rowText: "RequestId" },
            { headerText: "CreatedDate", rowText: "CreateDate" },
            {
                headerText: "Status", rowText: function (item) {
                    if (item.Status == 0)
                        return "New";
                    else if (item.Status == 1)
                    return "Approved";
                    else return "Rejected";
                }
            },
            { headerText: "AssemblyName", rowText: "AssemblyName" },
            { headerText: "Address", rowText: "Address1" },
            { headerText: "City", rowText: "City"},
            { headerText: "District", rowText: "District" },
            { headerText: "State", rowText: "State"  },
            { headerText: "Country", rowText: "Country" },
            {
                headerText: "Edit", rowText: {
                    action: function (item) {
                        return function () {
                            window.location.href = '/AssemblyRequests/Edit?id=' + item.RequestId;
                        }
                    }
                }
            },
            {
                headerText: "Approve", rowText: {
                    action: function (item) {
                        return function () {
                            if (item.Status == 0) {

                                try {
                                    $.ajax({
                                        url: '/ApproveAssemblyRequest?id=' + item.RequestId,
                                        type: 'GET',
                                        contentType: 'application/json',
                                        headers: {
                                            "Content-Type": "application/x-www-form-urlencoded",
                                            "Authorization": getAccessToken()
                                        },
                                        success: successCallback,
                                        error: errorCallback
                                    });
                                } catch (e) {
                                    console.log(e);
                                    alert(e);
                                }

                            } else {
                                alert("You can not update the status now. It is already approved/Rejected");
                            }
                        }
                    }
                }
            },
            {
                headerText: "Reject", rowText: {
                    action: function (item) {
                        return function () {
                            if (item.Status == 0) {

                                try {
                                    $.ajax({
                                        url: '/RejectAssemblyRequest?id=' + item.RequestId,
                                        type: 'GET',
                                        contentType: 'application/json',
                                        headers: {
                                            "Content-Type": "application/x-www-form-urlencoded",
                                            "Authorization": getAccessToken()
                                        },
                                        success: successrejectCallback,
                                        error: errorCallback
                                    });
                                } catch (e) {
                                    console.log(e);
                                    alert(e);
                                }

                            } else {
                                alert("You can not update the status now. It is already approved/Rejected");
                            }
                        }
                    }
                }
            }
        ],
        pageSize: 50
    });
};

function successCallback(data) {
    alert("Assembly request approved");
    window.location.href = '/AssemblyRequests/index';
}

function successrejectCallback() {
    alert("Assembly request rejected");
    window.location.href = '/AssemblyRequests/index';
}

function errorCallback(err) {
    console.log(err);
    alert(err);
    //window.location.href = '/Home/index1';
}

$(function () {

    try {
        $.ajax({
            url: '/AllAssemblyRequests/',
            type: 'GET',
            dataType: 'json',
            contentType: 'application/json',
            headers: {
                "Content-Type": "application/x-www-form-urlencoded",
                "Authorization": getAccessToken()
            },
            success: function (data) {
                console.log(data);
                ko.applyBindings(PagedGridModel(data));
            },
            error: function (err) {
                alert(err.status + " : " + err.statusText);
            }

        });
    }
    catch (e) {
        window.location.href = '/Employee/Index';
    }

});

function deleteAssembly(id) {
    try {
        $.ajax({
            url: '/Assemblies?id=' + id,
            type: 'PUT',
            dataType: 'json',
            contentType: 'application/json',
            headers: {
                "Content-Type": "application/x-www-form-urlencoded",
                "Authorization": getAccessToken()
            },
            success: function (data) {
                alert("Delete Successfully");
                window.location.href = '/Home/Index';
            },
            error: function (err) {
                alert(err.status + " : " + err.statusText);
            }

        });
    }
    catch (e) {
        window.location.href = '/Home/Index';
    }
}