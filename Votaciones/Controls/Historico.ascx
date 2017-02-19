<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Historico.ascx.cs" Inherits="Votaciones.Controls.Historico" %>

<div ng-app="appHist">
    <div ng-controller="ctrlHist as ctrl">
        <menu-historic></menu-historic>
        <div id="myCarouselBar" class="carousel slide box-shadowed" data-ride="carousel" >
            <!-- Wrapper for slides -->
            <div class="carousel-inner" role="listbox">
                <div ng-class="isActiveItem($index) " ng-repeat="item in globalDataBar">
                    <item-bar></item-bar>
                </div>
            </div>
            <!-- Left and right controls -->
            <a class="left carousel-control" href="#myCarousel" role="button" data-slide="prev">
                <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
                <span class="sr-only">Previous</span>
            </a>
            <a class="right carousel-control" href="#myCarousel" role="button" data-slide="next">
                <span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
                <span class="sr-only">Next</span>
            </a>
        </div>

        <div id="myCarouselComensal" class="carousel slide box-shadowed" data-ride="carousel">
            <!-- Wrapper for slides -->
            <div class="carousel-inner" role="listbox">
                <div ng-class="isActiveItem($index) " ng-repeat="item in globalDataComensal">
                    <item-comensal></item-comensal>
                </div>
            </div>
            <!-- Left and right controls -->
            <a class="left carousel-control" href="#myCarousel" role="button" data-slide="prev">
                <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
                <span class="sr-only">Previous</span>
            </a>
            <a class="right carousel-control" href="#myCarousel" role="button" data-slide="next">
                <span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
                <span class="sr-only">Next</span>
            </a>
        </div>
    </div>


</div>
<script>
    var myApp = angular.module("appHist", [])
    .controller("ctrlHist", ['$scope', 'getData', function ($scope, getData) {
        $scope.isActiveItem = function (index) {
            if (index == 0)
                return "item active";
            else
                return "item";
        };
        var data = { $type: "getHistoricBar" };
        $scope.globalDataBar = {};
        getData(data, function (res) {
            $scope.globalDataBar = JSON.parse(res);
            $scope.$apply();
        });

        var data = { $type: "getHistoricComensal" };
        $scope.globalDataComensal = {};
        getData(data, function (res) {
            $scope.globalDataComensal = JSON.parse(res);
            $scope.$apply();
        });


    }])
   .directive("itemBar",
      function () {
          return {
              restrict: "E",
              templateUrl: "/Templates/itemBar.html"
          }
      })
    .directive("menuHistoric",
      function () {
          return {
              restrict: "E",
              templateUrl: "/Templates/menuHistoric.html"
          }
      })
   .factory('getData',
         ['$http', function ($http) {
             return function (data, callback) {
                 $.post("/getter.ashx",
               { data: JSON.stringify(data) },
                   callback);
             }
         }]);
</script>
