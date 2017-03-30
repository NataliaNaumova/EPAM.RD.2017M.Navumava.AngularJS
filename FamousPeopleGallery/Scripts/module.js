angular.module('galleryApp', ['ngRoute', 'ngCookies'])
    .config([
        '$locationProvider', '$routeProvider',
        function($locationProvider, $routeProvider) {
            $routeProvider
                .when('/', {
                    templateUrl: '/views/Angular/main.html',
                    controller: 'MainController'
                })
                .when('/Angular/register', {
                    templateUrl: '/Views/Angular/register.html',
                    controller: 'registerController'
                })
                .when('/Angular/login', {
                    templateUrl: '/Views/Angular/login.html',
                    controller: 'loginController'
                })
                .when('/Angular/addPhoto', {
                    templateUrl: '/Views/Angular/addImage.html',
                    controller: 'addImageController'
                })
                .when('/Angular/gallery', {
                    templateUrl: '/Views/Angular/gallery.html',
                    controller: 'galleryController'
                })
                .otherwise({
                    redirectTo: '/'
                });

            $locationProvider.html5Mode(true);
        }
    ])
    .controller('MainController', [
        '$scope', function($scope) {
        }
    ])
    .controller('galleryController',
    [
        '$scope', '$http', 'dataCenter', '$rootScope',
        function($scope, $http, dataCenter, $rootScope) {
            $scope.albums = [];

            dataCenter.getAlbumNames().then(function(response) {
                $scope.albums = response.data;
                $scope.currentAlbum = $scope.albums[0];

                $scope.changeAlbum();
            });

            $scope.changeAlbum = function() {
                dataCenter.getAlbum($scope.currentAlbum.Id).then(function(response) {

                    if ($rootScope.userId != null) {
                        angular.forEach(response.data, function(value, key) {
                            value.hasLike = false;
                            angular.forEach(value.Likes, function(likeValue, key) {
                                if (likeValue.ProfileId == $rootScope.userId)
                                    value.hasLike = true;
                            });
                        });
                    }

                    $scope.currentAlbum.Images = response.data;
                });
            };

            $scope.setLike = function(image) {
                $http.post(
                    '/Like/Like',
                    {
                        photoId: image.Id
                    }).then(
                    function(response) {
                        image.Likes = response.data;
                        image.hasLike = !image.hasLike;
                    });
            }

        }
    ])
    .controller('navigationController', [
        '$scope', 'dataCenter', function($scope, dataCenter) {
            dataCenter.refreshSession();
        }
    ])
    .controller('addImageController', [
        '$scope', 'dataCenter', '$http', ' $location', function($scope, dataCenter, $http, $location) {
            $scope.albums = [];

            dataCenter.getAlbumNames().then(function(response) {
                response.data.splice(0, 1);
                $scope.albums = response.data;
                $scope.currentAlbum = $scope.albums[0];
            });

            $scope.imageUpload = function(event) {
                var file = event.target.files[0];
                var reader = new FileReader();
                reader.onload = $scope.imageIsLoaded;
                reader.readAsDataURL(file);
            };

            $scope.imageIsLoaded = function(e) {
                $scope.$apply(function() {
                    $scope.imageToAdd = e.target.result;
                });
            };

            $scope.addImage = function(photo, currentAlbum) {
                $http.post(
                    '/Photo/Add',
                    {
                        name: photo.name,
                        data: $scope.imageToAdd,
                        albumId: currentAlbum.Id
                    }).then(
                    function(response) {
                        if (response.data) {
                            $location.path('/Angular/gallery');
                        } else {
                            $scope.serverResponseError = true;
                        }
                    },
                    function(response) {
                        $scope.serverResponseError = true;
                        $scope.serverResponseErrorMessage = "Sorry. Service is inavailable. Try again later.";
                    });

            }

        }
    ])
    .controller('registerController', [
        '$scope', '$http', '$location', function($scope, $http, $location) {

            $scope.serverResponseError = false;
            $scope.serverResponseErrorMessage = null;

            $scope.register = function(user) {
                $http.post(
                    '/Account/Register',
                    {
                        email: user.email,
                        login: user.login,
                        password: user.password
                    }).then(
                    function(response) {
                        if (response.data.success) {
                            $location.path('/');
                        } else {
                            $scope.serverResponseError = true;
                            $scope.serverResponseErrorMessage = response.data.message;
                        }
                    },
                    function(response) {
                        $scope.serverResponseError = true;
                        $scope.serverResponseErrorMessage = "Sorry. Service is inavailable. Try again later.";
                    });

            }
        }
    ])
    .controller('loginController', [
        '$scope', '$http', '$location', 'dataCenter', function($scope, $http, $location, dataCenter) {

            $scope.serverResponseError = false;
            $scope.serverResponseErrorMessage = null;

            $scope.login = function(user) {
                $http.post(
                    '/Account/Login',
                    {
                        login: user.login,
                        password: user.password
                    }).then(
                    function(response) {
                        if (response.data.success) {
                            dataCenter.refreshSession();
                            $location.path('/');
                        } else {
                            $scope.serverResponseError = true;
                            $scope.serverResponseErrorMessage = response.data.message;
                        }
                    },
                    function(response) {
                        $scope.serverResponseError = true;
                        $scope.serverResponseErrorMessage = "Sorry. Service is inavailable. Try again later.";
                    });

            }
        }
    ])
    .controller('logoutController', [
        '$scope', '$http', 'dataCenter', function($scope, $http, dataCenter) {

            $scope.logout = function() {
                $http.post(
                    '/Account/LogOut',
                    null).then(
                    function() {
                        dataCenter.refreshSession();
                    },
                    function() {
                        alert("Sorry. Service is inavailable. Try again later.");
                    });

            }
        }
    ])
    .service('dataCenter', [
        '$http', '$cookies', '$rootScope', function($http, $cookies, $rootScope) {

            function refreshSession() {
                $rootScope.userId = $cookies.get("user_id");
                $rootScope.role = $cookies.get("role");
                refreshLogin();
            };

            function refreshLogin() {
                $http.post(
                    '/Account/GetUserLogin',
                    null).then(
                    function(response) {
                        $rootScope.userLogin = response.data.login;
                    },
                    function() {
                        $rootScope.userLogin = null;
                    });
            }

            function getAllImages() {
                var response = $http.get(
                    '/Photo/GetAllPhotos'
                );

                return response;
            };

            function getAlbum(selectedAlbumId) {
                var url = '/Photo/GetAlbumPhotos' + '/' + selectedAlbumId;
                var response = $http.get(url);

                return response;
            };

            function getAlbumNames() {
                var response = $http.get('/Photo/GetAlbumNames');

                return response;
            };


            return {
                refreshSession: refreshSession,
                getAllImages: getAllImages,
                getAlbum: getAlbum,
                getAlbumNames: getAlbumNames
            };
        }
    ])
    .directive('pwCheck', function() {
        return {
            require: 'ngModel',
            scope: {
                confirmPassword: '=pwCheck'
            },
            link: function(scope, element, attributes, ngModel) {

                ngModel.$validators.pwCheck = function(password) {
                    return password === scope.confirmPassword;
                };

                scope.$watch('confirmPassword', function() {
                    ngModel.$validate();
                });
            }
        };
    });