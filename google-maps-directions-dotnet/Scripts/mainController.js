angular.module("gdirApp", ["ngResource", "customFilters"])
.constant("baseUrl", "/api/directions")
.controller("mainController", function ($scope, $http, $resource, baseUrl) {

    // location object should have string members for "from" and "to" so we know where we start from and where we go to.
	// These will be passed to the API as query string parameters.
    $scope.location = null;
	
	// directions object that is returned from the server will have members:
	//     distance (String)
	//     duration (String)
	//     errorMessage (String)
	//     steps (Array of String)
	$scope.directions = null;

	// resource that we use to call the API
    $scope.directionsResource = $resource(baseUrl);

	// method to get the directions from the API
    $scope.getDirections = function () {
        $scope.directions = $scope.directionsResource.get($scope.location, function(response) {
       		console.log(response);
       		if ("errorMessage" in response)
       		{
				$scope.directions = null;
       			alert(response.errorMessage);
       		}
        }, function(error) {
        	alert("Error connecting to server. Please try again later.");
        });
    }
	
});
