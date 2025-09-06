module.exports = class ApiError {
    constructor(status, message = '', errors = []){
        this.status = status;
        this.message = message;
        this.errors = errors;
    }

    static UnauthorizedError(){
        return new ApiError(401, "User unauthorized");
    };

    static ForbiddenError(){
        return new ApiError(403, "Forbidden");
    };

    static BadRequestError(message, errors){
        return new ApiError(400, message, errors);
    };

    static NotFoundError(message){
        return new ApiError(404, message);
    };
};