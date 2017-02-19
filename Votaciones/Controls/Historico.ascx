<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Historico.ascx.cs" Inherits="Votaciones.Controls.Historico" %>

<div ng-app="appHist">
    <div ng-controller="ctrlHist as ctrl">

        <div class="row">
                            <div class="col-md-12">
                                <ul class="nav nav-tabs">
                                    <li class="dropdown pull-right">
                                        <a href="#" data-toggle="dropdown" class="dropdown-toggle">Dropdown<strong class="caret"></strong></a>
                                        <ul class="dropdown-menu">
                                            <li>
                                                <a href="#culo">Action</a>
                                            </li>
                                            <li>
                                                <a href="#">Another action</a>
                                            </li>
                                            <li>
                                                <a href="#">Something else here</a>
                                            </li>
                                            <li class="divider"></li>
                                            <li>
                                                <a href="#">Separated link</a>
                                            </li>
                                        </ul>
                                    </li>
                                </ul>
                            </div>
                        </div>
        <div id="myCarousel" class="carousel slide box-shadowed" data-ride="carousel">         
            <!-- Wrapper for slides -->
            <div class="carousel-inner" role="listbox" >
                    <div ng-class="isActiveItem($index) " ng-repeat="item in globalData">                       
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

                var data = { $type: "getHistoric" };
                $scope.globalData = {};
                getData(data, function (res) {                    
                    $scope.globalData = JSON.parse(res);
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
           .factory('getData',
                 ['$http', function ($http) {
                     return function (data, callback) {
                         $.post("/getter.ashx",
                       { data: JSON.stringify(data) },
                           callback);
                     }
                 }]);
        </script>
