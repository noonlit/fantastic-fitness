"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var testing_1 = require("@angular/core/testing");
var subscriptions_service_1 = require("./subscriptions.service");
describe('SubscriptionsService', function () {
    beforeEach(function () { return testing_1.TestBed.configureTestingModule({}); });
    it('should be created', function () {
        var service = testing_1.TestBed.get(subscriptions_service_1.SubscriptionsService);
        expect(service).toBeTruthy();
    });
});
//# sourceMappingURL=subscriptions.service.spec.js.map