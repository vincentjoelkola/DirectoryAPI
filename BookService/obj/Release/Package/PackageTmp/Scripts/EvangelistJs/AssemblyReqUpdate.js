$(function () {
    ko.applyBindings(modelUpdate);
    modelUpdate.getAvailableServices();
    modelUpdate.getAssemblyDetails(requestId);
});

var modelUpdate = {
    getAssemblyDetails: function (requestId) {
        var thisObj = this;
        try {
            $.ajax({
                url: '/AssemblyRequest?id=' + requestId,
                type: 'GET',
                dataType: 'json',
                contentType: 'application/json',
                headers: {
                    "Content-Type": "application/x-www-form-urlencoded",
                    "Authorization": getAccessToken()
                },
                success: function (data) {
                    console.log(data);
                    thisObj.AssemblyId(data.AssemblyId);
                    thisObj.AssemblyName(data.AssemblyName);
                    thisObj.Status(data.Status);
                    thisObj.WorshipTime(data.WorshipTime);
                    thisObj.NoOfPersons(data.NoOfPersons);
                    thisObj.Address1(data.Address1);
                    thisObj.LandMark(data.LandMark);
                    thisObj.City(data.City);
                    thisObj.District(data.District);
                    thisObj.State(data.State);
                    thisObj.Country(data.Country);
                    thisObj.PinCode(data.PinCode);
                    thisObj.EmailAddress(data.EmailAddress);
                    thisObj.PermanantPhoneNo(data.PermanantPhoneNo);
                    thisObj.ServiceLanguages(data.ServiceLanguages);
                    thisObj.AssemblyElders(data.AssemblyElders);
                },
                error: function (err) {
                    alert(err.status + " : " + err.statusText);
                }

            });
        } catch (e) {
            window.location.href = '/Employee/Index';
        }
    },
    getAvailableServices: function () {
        var thisObj = this;
        try {
            $.ajax({
                url: '/AvailableServices/',
                type: 'GET',
                dataType: 'json',
                contentType: 'application/json',
                headers: {
                    "Content-Type": "application/x-www-form-urlencoded",
                    "Authorization": getAccessToken()
                },
                success: function (data) {
                    console.log("Available Services");
                    console.log(data);
                    thisObj.AvailableServices(data);//Here we are assigning values to KO Observable array
                },
                error: function (err) {
                    alert(err.status + " : " + err.statusText);
                }

            });
        } catch (e) {
            window.location.href = '/Employee/Index';
        }
    },
    //Update
    AssemblyId: ko.observable(),
    AssemblyName: ko.observable(),
    Status: ko.observable(),
    WorshipTime: ko.observable(),
    AvailableServices: ko.observableArray(),
    ServiceLanguages: ko.observableArray(),
    NoOfPersons: ko.observable(),
    Address1: ko.observable(),
    LandMark: ko.observable(),
    City: ko.observable(),
    District: ko.observable(),
    State: ko.observable(),
    Country: ko.observable(),
    PinCode: ko.observable(),
    EmailAddress: ko.observable(),
    PermanantPhoneNo: ko.observable(),
    AssemblyElders: ko.observableArray(),

    approveRequest: function () {
        //if (this.Status() != 0) {
        //    alert("You can not update the status now. It is already approved/Rejected");
        //    return;
        //}

        var updateData = {
            RequestId: requestId,
            AssemblyId: this.AssemblyId,
            AssemblyName: this.AssemblyName,
            WorshipTime: this.WorshipTime,
            ServiceLanguages: ko.toJS(this.ServiceLanguages),
            NoOfPersons: this.NoOfPersons,
            Address1: this.Address1,
            LandMark: this.LandMark,
            City: this.City,
            District: this.District,
            State: this.State,
            Country: this.Country,
            PinCode: this.PinCode,
            EmailAddress: this.EmailAddress,
            PermanantPhoneNo: this.PermanantPhoneNo,
            AssemblyElders: ko.toJS(this.AssemblyElders),
        }; 

        try {
            $.ajax({
                url: '/SaveEditedAssemblyRequest/',
                type: 'POST',
                dataType: 'json',
                data: updateData,
                contentType: 'application/json',
                headers: {
                    "Content-Type": "application/x-www-form-urlencoded",
                    "Authorization": getAccessToken()
                },
                success: successCallback,
                error: errorCallback
            });
        } catch (e) {
            alert(e);
        }
    },

    rejectRequest: function () {

        //if (this.Status() != 0) {
        //    alert("You can not update the status now. It is already approved/Rejected");
        //    return;
        //}

        try {
            $.ajax({
                url: '/RejectAssemblyRequest?id=' + requestId,
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
    },

    cancel: function () {
        window.location.href = '/AssemblyRequests/index';
    },

    addElder:function ()
    {
        this.AssemblyElders.push(new newElder());
    },

    deleteElder: function () {
        modelUpdate.AssemblyElders.remove(this);
    }
    //End update here   
}

var newElder = function () {
    var self = this;
    self.ElderId = 0;
    self.ElderName = ko.observable();
    self.ElderPhone = ko.observable();
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

function cancel() {
    window.location.href = '/AssemblyRequests/index';
}