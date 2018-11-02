'use strict';
app.service('schemaService', [
    '$http', 'getColumnsUrl', function ($http, getColumnsUrl) {

        this.getColumns = function () {
            return $http.get(getColumnsUrl);
        }

    }
]);