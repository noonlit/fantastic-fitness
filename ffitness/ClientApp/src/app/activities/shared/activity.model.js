"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ACTIVITY_TYPES = exports.ActivityType = exports.Activity = void 0;
var Activity = /** @class */ (function () {
    function Activity() {
    }
    return Activity;
}());
exports.Activity = Activity;
var ActivityType;
(function (ActivityType) {
    ActivityType[ActivityType["Cardio"] = 0] = "Cardio";
    ActivityType[ActivityType["Aerobic"] = 1] = "Aerobic";
    ActivityType[ActivityType["Strength"] = 2] = "Strength";
    ActivityType[ActivityType["Yoga"] = 3] = "Yoga";
    ActivityType[ActivityType["Flexibility"] = 4] = "Flexibility";
    ActivityType[ActivityType["Endurance"] = 5] = "Endurance";
    ActivityType[ActivityType["HIIT"] = 6] = "HIIT";
})(ActivityType = exports.ActivityType || (exports.ActivityType = {}));
exports.ACTIVITY_TYPES = ['Cardio', 'Aerobic', 'Strength', 'Yoga', 'Flexibility', 'Endurance', 'HIIT'];
//# sourceMappingURL=activity.model.js.map