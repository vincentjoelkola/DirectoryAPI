/*$(function () {
    ko.applyBindings(modelView);    
    modelView.viewEvangelists();
 
});

var modelView = {
    Evangelists: ko.observableArray([]),
    viewEvangelists: function () {
        var thisObj = this;
        try {
            $.ajax({
                url: '/Evangelists/',
                type: 'GET',
                dataType: 'json',
                contentType: 'application/json',
                success: function (data) {
                    thisObj.Evangelists(data); //Here we are assigning values to KO Observable array
                },
                error: function (err) {
                    alert(err.status + " : " + err.statusText);
                }

            });
        } catch (e) {
            window.location.href = '/Employee/Index';
        }
    },
}

function successCallback(data) {
    window.location.href = '/Employee/Index/';
}
function errorCallback(err) {
    window.location.href = '/Employee/Index/';
}*/

var PagedGridModel = function (items) {
    this.items = ko.observableArray(items);

    this.getAssemblies = function () {
        this.items.push({ name: "New item", sales: 0, price: 100 });
    };

    this.addItem = function () {
        this.items.push({ name: "New item", sales: 0, price: 100 });
    };
    this.isAscOrder = false;
    this.sortByName = function () {
        if (this.isAscOrder) {
            this.items.sort(function (a, b) {
                return b.Name < a.Name ? -1 : 1;
            });
            this.isAscOrder = false;
        }
        else {
            this.items.sort(function (a, b) {
                return a.Name < b.Name ? -1 : 1;
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
            { headerText: "Evangelist Name", rowText: "Name" },
            { headerText: "Assembly Name", rowText: "AssemblyName" },
            { headerText: "Age", rowText: "Age" },
            { headerText: "Address", rowText: "Address1" },
            { headerText: "City", rowText: "City" },
            { headerText: "District", rowText: "District" },
            { headerText: "State", rowText: "State" },
            { headerText: "Country", rowText: "Country" },
            /*{ headerText: "Edit", rowText: function (item) { return "$" + item.price.toFixed(2) } },*/
            {
                headerText: "Edit", rowText: {
                    action: function (item) {
                        return function () {
                            window.location.href = '/Evangelist/Edit?id=' + item.EvangelistId;
                        }
                    }
                }
            },
            {
                headerText: "Delete", rowText: {
                    action: function (item) {
                        return function () {
                            console.log(item);
                            deleteEvangelist(item.EvangelistId);
                        }
                    }
                }
            }
        ],
        pageSize: 50
    });
};

$(function () {
    try {
        $.ajax({
            url: '/Evangelists?pagesize=5000',
            type: 'GET',
            dataType: 'json',
            contentType: 'application/json',
            headers: {
                "Content-Type": "application/x-www-form-urlencoded",
                "Authorization": getAccessToken()
            },
            success: function (data) {
                console.log(data);
                ko.applyBindings(PagedGridModel(data.Evangelists));
            },
            error: function (err) {
                alert(err.status + " : " + err.statusText);
            }

        });
    } catch (e) {
        window.location.href = '/Evangelist/Index';
    }
});

function deleteEvangelist(id) {
    try {
        $.ajax({
            url: '/Evangelists?id=' + id,
            type: 'PUT',
            dataType: 'json',
            contentType: 'application/json',
            headers: {
                "Content-Type": "application/x-www-form-urlencoded",
                "Authorization": getAccessToken()
            },
            success: function (data) {
                alert("Delete Successfully");
                window.location.href = '/Evangelist/Index';
            },
            error: function (err) {
                alert(err.status + " : " + err.statusText);
            }

        });
    }
    catch (e) {
        alert(e);
        window.location.href = '/Evangelist/Index';
    }
}