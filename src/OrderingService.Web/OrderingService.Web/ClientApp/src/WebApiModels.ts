// tslint:disable

import * as request from "superagent";
import {
    SuperAgentStatic,
    SuperAgentRequest,
    Response
} from "superagent";

export type RequestHeaders = {
    [header: string]: string;
}
export type RequestHeadersHandler = (headers: RequestHeaders) => RequestHeaders;

export type ConfigureAgentHandler = (agent: SuperAgentStatic) => SuperAgentStatic;

export type ConfigureRequestHandler = (agent: SuperAgentRequest) => SuperAgentRequest;

export type CallbackHandler = (err: any, res ? : request.Response) => void;

export type UserAuthDto = {
    'email' ? : string;
    'phoneNumber' ? : string;
    'role' ? : string;
    'firstName' ? : string;
    'lastName' ? : string;
    'imagePublicId' ? : string;
    'id': string;
    'employeeProfile' ? : EmployeeProfileDto;
    'token' ? : string;
};

export type UserDto = {
    'email' ? : string;
    'phoneNumber' ? : string;
    'role' ? : string;
    'firstName' ? : string;
    'lastName' ? : string;
    'imagePublicId' ? : string;
    'id': string;
    'employeeProfile' ? : EmployeeProfileDto;
};

export type EmployeeProfileDto = {
    'serviceType' ? : string;
    'serviceCost': number;
    'description' ? : string;
    'id': string;
    'userId': string;
    'user' ? : UserDto;
    'averageRate' ? : number;
    'reviewsCount': number;
};

export type EmployeeProfileDtoBase = {
    'serviceType' ? : string;
    'serviceCost': number;
    'description' ? : string;
} & {
    [key: string]: any;
};

export type UserDtoBase = {
    'email' ? : string;
    'phoneNumber' ? : string;
    'role' ? : string;
    'firstName' ? : string;
    'lastName' ? : string;
    'imagePublicId' ? : string;
} & {
    [key: string]: any;
};

export type ValidationProblemDetails = {
    'type' ? : string;
    'title' ? : string;
    'status' ? : number;
    'detail' ? : string;
    'instance' ? : string;
    'extensions' ? : {} & {
        [key: string]: void;
    };
    'errors' ? : {} & {
        [key: string]: Array < string >
        ;
    };
};

export type ProblemDetails = {
    'type' ? : string;
    'title' ? : string;
    'status' ? : number;
    'detail' ? : string;
    'instance' ? : string;
    'extensions' ? : {} & {
        [key: string]: void;
    };
} & {
    [key: string]: any;
};

export type UserLoginModel = {
    'userEmail' ? : string;
    'userPassword' ? : string;
} & {
    [key: string]: any;
};

export type UserCreateDto = {
    'email' ? : string;
    'phoneNumber' ? : string;
    'role' ? : string;
    'firstName' ? : string;
    'lastName' ? : string;
    'imagePublicId' ? : string;
    'password' ? : string;
};

export type IPagedResultOfEmployeeProfileDto = {
    'value' ? : Array < EmployeeProfileDto >
    ;
    'pagesCount': number;
    'pageSize': number;
    'pageNumber': number;
    'total': number;
} & {
    [key: string]: any;
};

export type EmployeeProfileCreateDto = {
    'serviceType' ? : string;
    'serviceCost': number;
    'description' ? : string;
    'userId': string;
};

export type EmployeeProfileUpdateDto = {
    'serviceType' ? : string;
    'serviceCost': number;
    'description' ? : string;
    'id': string;
};

export type IPagedResultOfOrderDto = {
    'value' ? : Array < OrderDto >
    ;
    'pagesCount': number;
    'pageSize': number;
    'pageNumber': number;
    'total': number;
} & {
    [key: string]: any;
};

export type OrderDto = {
    'clientId': string;
    'employeeId': string;
    'briefTask' ? : string;
    'serviceDetails' ? : string;
    'address' ? : string;
    'contactPhone' ? : string;
    'price': number;
    'id': number;
    'client' ? : UserDto;
    'employee' ? : EmployeeProfileDto;
    'date': string;
    'status': OrderStatus;
};

export type OrderStatus = 0 | 1 | 2 | 3;

export type OrderCreateDto = {
    'clientId': string;
    'employeeId': string;
    'briefTask' ? : string;
    'serviceDetails' ? : string;
    'address' ? : string;
    'contactPhone' ? : string;
    'price': number;
} & {
    [key: string]: any;
};

export type IPagedResultOfReviewDto = {
    'value' ? : Array < ReviewDto >
    ;
    'pagesCount': number;
    'pageSize': number;
    'pageNumber': number;
    'total': number;
} & {
    [key: string]: any;
};

export type ReviewDto = {
    'text' ? : string;
    'rate': number;
    'clientId': string;
    'employeeId': string;
    'id': number;
    'date': string;
    'client' ? : UserDto;
    'employee' ? : EmployeeProfileDto;
};

export type ReviewCreateDto = {
    'text' ? : string;
    'rate': number;
    'clientId': string;
    'employeeId': string;
} & {
    [key: string]: any;
};

export type ServiceTypeDto = {
    'id': number;
    'name' ? : string;
} & {
    [key: string]: any;
};

export type Response_ServiceType_GetAllOrderedByProfilesCount_200 = Array < ServiceTypeDto >
;

export type Logger = {
    log: (line: string) => any
};

export interface ResponseWithBody < S extends number, T > extends Response {
    status: S;
    body: T;
}

export type QueryParameters = {
    [param: string]: any
};

export interface CommonRequestOptions {
    $queryParameters ? : QueryParameters;
    $domain ? : string;
    $path ? : string | ((path: string) => string);
    $retries ? : number; // number of retries; see: https://github.com/visionmedia/superagent/blob/master/docs/index.md#retrying-requests
    $timeout ? : number; // request timeout in milliseconds; see: https://github.com/visionmedia/superagent/blob/master/docs/index.md#timeouts
    $deadline ? : number; // request deadline in milliseconds; see: https://github.com/visionmedia/superagent/blob/master/docs/index.md#timeouts
}

/**
 * 
 * @class SwaggerCodegen
 * @param {(string)} [domainOrOptions] - The project domain.
 */
export class SwaggerCodegen {

    private domain: string = "";
    private errorHandlers: CallbackHandler[] = [];
    private requestHeadersHandler ? : RequestHeadersHandler;
    private configureAgentHandler ? : ConfigureAgentHandler;
    private configureRequestHandler ? : ConfigureRequestHandler;

    constructor(domain ? : string, private logger ? : Logger) {
        if (domain) {
            this.domain = domain;
        }
    }

    getDomain() {
        return this.domain;
    }

    addErrorHandler(handler: CallbackHandler) {
        this.errorHandlers.push(handler);
    }

    setRequestHeadersHandler(handler: RequestHeadersHandler) {
        this.requestHeadersHandler = handler;
    }

    setConfigureAgentHandler(handler: ConfigureAgentHandler) {
        this.configureAgentHandler = handler;
    }

    setConfigureRequestHandler(handler: ConfigureRequestHandler) {
        this.configureRequestHandler = handler;
    }

    private request(method: string, url: string, body: any, headers: RequestHeaders, queryParameters: QueryParameters, form: any, reject: CallbackHandler, resolve: CallbackHandler, opts: CommonRequestOptions) {
        if (this.logger) {
            this.logger.log(`Call ${method} ${url}`);
        }

        const agent = this.configureAgentHandler ?
            this.configureAgentHandler(request.default) :
            request.default;

        let req = agent(method, url);
        if (this.configureRequestHandler) {
            req = this.configureRequestHandler(req);
        }

        req = req.query(queryParameters);

        if (body) {
            req.send(body);

            if (typeof(body) === 'object' && !(body.constructor.name === 'Buffer')) {
                headers['Content-Type'] = 'application/json';
            }
        }

        if (Object.keys(form).length > 0) {
            req.type('form');
            req.send(form);
        }

        if (this.requestHeadersHandler) {
            headers = this.requestHeadersHandler({
                ...headers
            });
        }

        req.set(headers);

        if (opts.$retries && opts.$retries > 0) {
            req.retry(opts.$retries);
        }

        if (opts.$timeout && opts.$timeout > 0 || opts.$deadline && opts.$deadline > 0) {
            req.timeout({
                deadline: opts.$deadline,
                response: opts.$timeout
            });
        }

        req.end((error, response) => {
            // an error will also be emitted for a 4xx and 5xx status code
            // the error object will then have error.status and error.response fields
            // see superagent error handling: https://github.com/visionmedia/superagent/blob/master/docs/index.md#error-handling
            if (error) {
                reject(error);
                this.errorHandlers.forEach(handler => handler(error));
            } else {
                resolve(response);
            }
        });
    }

    private convertParameterCollectionFormat < T > (param: T, collectionFormat: string | undefined): T | string {
        if (Array.isArray(param) && param.length >= 2) {
            switch (collectionFormat) {
                case "csv":
                    return param.join(",");
                case "ssv":
                    return param.join(" ");
                case "tsv":
                    return param.join("\t");
                case "pipes":
                    return param.join("|");
                default:
                    return param;
            }
        }

        return param;
    }

    Account_AuthURL(parameters: {
        'loginModel': UserLoginModel,
    } & CommonRequestOptions): string {
        let queryParameters: QueryParameters = {};
        const domain = parameters.$domain ? parameters.$domain : this.domain;
        let path = '/api/Account/auth';
        if (parameters.$path) {
            path = (typeof(parameters.$path) === 'function') ? parameters.$path(path) : parameters.$path;
        }

        if (parameters.$queryParameters) {
            queryParameters = {
                ...queryParameters,
                ...parameters.$queryParameters
            };
        }

        queryParameters = {};

        let keys = Object.keys(queryParameters);
        return domain + path + (keys.length > 0 ? '?' + (keys.map(key => key + '=' + encodeURIComponent(queryParameters[key])).join('&')) : '');
    }

    /**
     * 
     * @method
     * @name SwaggerCodegen#Account_Auth
     * @param {} loginModel - 
     */
    Account_Auth(parameters: {
        'loginModel': UserLoginModel,
    } & CommonRequestOptions): Promise < ResponseWithBody < 200, UserAuthDto > | ResponseWithBody < 400, ValidationProblemDetails >> {
        const domain = parameters.$domain ? parameters.$domain : this.domain;
        let path = '/api/Account/auth';
        if (parameters.$path) {
            path = (typeof(parameters.$path) === 'function') ? parameters.$path(path) : parameters.$path;
        }

        let body: any;
        let queryParameters: QueryParameters = {};
        let headers: RequestHeaders = {};
        let form: any = {};
        return new Promise((resolve, reject) => {
            headers['Accept'] = 'text/plain, application/json, text/json';
            headers['Content-Type'] = 'application/json-patch+json';

            if (parameters['loginModel'] !== undefined) {
                body = parameters['loginModel'];
            }

            if (parameters['loginModel'] === undefined) {
                reject(new Error('Missing required  parameter: loginModel'));
                return;
            }

            if (parameters.$queryParameters) {
                queryParameters = {
                    ...queryParameters,
                    ...parameters.$queryParameters
                };
            }

            form = queryParameters;
            queryParameters = {};

            this.request('POST', domain + path, body, headers, queryParameters, form, reject, resolve, parameters);
        });
    }

    Account_SignUpURL(parameters: {
        'userDto': UserCreateDto,
    } & CommonRequestOptions): string {
        let queryParameters: QueryParameters = {};
        const domain = parameters.$domain ? parameters.$domain : this.domain;
        let path = '/api/Account/sign-up';
        if (parameters.$path) {
            path = (typeof(parameters.$path) === 'function') ? parameters.$path(path) : parameters.$path;
        }

        if (parameters.$queryParameters) {
            queryParameters = {
                ...queryParameters,
                ...parameters.$queryParameters
            };
        }

        queryParameters = {};

        let keys = Object.keys(queryParameters);
        return domain + path + (keys.length > 0 ? '?' + (keys.map(key => key + '=' + encodeURIComponent(queryParameters[key])).join('&')) : '');
    }

    /**
     * 
     * @method
     * @name SwaggerCodegen#Account_SignUp
     * @param {} userDto - 
     */
    Account_SignUp(parameters: {
        'userDto': UserCreateDto,
    } & CommonRequestOptions): Promise < ResponseWithBody < 200, UserAuthDto > | ResponseWithBody < 400, ValidationProblemDetails >> {
        const domain = parameters.$domain ? parameters.$domain : this.domain;
        let path = '/api/Account/sign-up';
        if (parameters.$path) {
            path = (typeof(parameters.$path) === 'function') ? parameters.$path(path) : parameters.$path;
        }

        let body: any;
        let queryParameters: QueryParameters = {};
        let headers: RequestHeaders = {};
        let form: any = {};
        return new Promise((resolve, reject) => {
            headers['Accept'] = 'text/plain, application/json, text/json';
            headers['Content-Type'] = 'application/json-patch+json';

            if (parameters['userDto'] !== undefined) {
                body = parameters['userDto'];
            }

            if (parameters['userDto'] === undefined) {
                reject(new Error('Missing required  parameter: userDto'));
                return;
            }

            if (parameters.$queryParameters) {
                queryParameters = {
                    ...queryParameters,
                    ...parameters.$queryParameters
                };
            }

            form = queryParameters;
            queryParameters = {};

            this.request('POST', domain + path, body, headers, queryParameters, form, reject, resolve, parameters);
        });
    }

    Account_GetProfileURL(parameters: {} & CommonRequestOptions): string {
        let queryParameters: QueryParameters = {};
        const domain = parameters.$domain ? parameters.$domain : this.domain;
        let path = '/api/Account/profile';
        if (parameters.$path) {
            path = (typeof(parameters.$path) === 'function') ? parameters.$path(path) : parameters.$path;
        }

        if (parameters.$queryParameters) {
            queryParameters = {
                ...queryParameters,
                ...parameters.$queryParameters
            };
        }

        let keys = Object.keys(queryParameters);
        return domain + path + (keys.length > 0 ? '?' + (keys.map(key => key + '=' + encodeURIComponent(queryParameters[key])).join('&')) : '');
    }

    /**
     * 
     * @method
     * @name SwaggerCodegen#Account_GetProfile
     */
    Account_GetProfile(parameters: {} & CommonRequestOptions): Promise < ResponseWithBody < 200, UserDto > | ResponseWithBody < 400, ValidationProblemDetails > | ResponseWithBody < 401, ProblemDetails >> {
        const domain = parameters.$domain ? parameters.$domain : this.domain;
        let path = '/api/Account/profile';
        if (parameters.$path) {
            path = (typeof(parameters.$path) === 'function') ? parameters.$path(path) : parameters.$path;
        }

        let body: any;
        let queryParameters: QueryParameters = {};
        let headers: RequestHeaders = {};
        let form: any = {};
        return new Promise((resolve, reject) => {
            headers['Accept'] = 'text/plain, application/json, text/json';

            if (parameters.$queryParameters) {
                queryParameters = {
                    ...queryParameters,
                    ...parameters.$queryParameters
                };
            }

            this.request('GET', domain + path, body, headers, queryParameters, form, reject, resolve, parameters);
        });
    }

    Account_UpdateProfileURL(parameters: {
        'id': string,
        'userDto': UserDto,
    } & CommonRequestOptions): string {
        let queryParameters: QueryParameters = {};
        const domain = parameters.$domain ? parameters.$domain : this.domain;
        let path = '/api/Account/{id}';
        if (parameters.$path) {
            path = (typeof(parameters.$path) === 'function') ? parameters.$path(path) : parameters.$path;
        }

        path = path.replace(
            '{id}',
            `${encodeURIComponent(this.convertParameterCollectionFormat(
                        parameters['id'],
                        ''
                    ).toString())}`
        );

        if (parameters.$queryParameters) {
            queryParameters = {
                ...queryParameters,
                ...parameters.$queryParameters
            };
        }

        let keys = Object.keys(queryParameters);
        return domain + path + (keys.length > 0 ? '?' + (keys.map(key => key + '=' + encodeURIComponent(queryParameters[key])).join('&')) : '');
    }

    /**
     * 
     * @method
     * @name SwaggerCodegen#Account_UpdateProfile
     * @param {string} id - 
     * @param {} userDto - 
     */
    Account_UpdateProfile(parameters: {
        'id': string,
        'userDto': UserDto,
    } & CommonRequestOptions): Promise < ResponseWithBody < 200, UserDto > | ResponseWithBody < 400, ProblemDetails > | ResponseWithBody < 401, ProblemDetails > | ResponseWithBody < 404, ValidationProblemDetails >> {
        const domain = parameters.$domain ? parameters.$domain : this.domain;
        let path = '/api/Account/{id}';
        if (parameters.$path) {
            path = (typeof(parameters.$path) === 'function') ? parameters.$path(path) : parameters.$path;
        }

        let body: any;
        let queryParameters: QueryParameters = {};
        let headers: RequestHeaders = {};
        let form: any = {};
        return new Promise((resolve, reject) => {
            headers['Accept'] = 'text/plain, application/json, text/json';
            headers['Content-Type'] = 'application/json-patch+json';

            path = path.replace(
                '{id}',
                `${encodeURIComponent(this.convertParameterCollectionFormat(
                    parameters['id'],
                    ''
                ).toString())}`
            );

            if (parameters['id'] === undefined) {
                reject(new Error('Missing required  parameter: id'));
                return;
            }

            if (parameters['userDto'] !== undefined) {
                body = parameters['userDto'];
            }

            if (parameters['userDto'] === undefined) {
                reject(new Error('Missing required  parameter: userDto'));
                return;
            }

            if (parameters.$queryParameters) {
                queryParameters = {
                    ...queryParameters,
                    ...parameters.$queryParameters
                };
            }

            this.request('PUT', domain + path, body, headers, queryParameters, form, reject, resolve, parameters);
        });
    }

    EmployeeProfile_GetEmployeeByIdURL(parameters: {
        'id': string,
    } & CommonRequestOptions): string {
        let queryParameters: QueryParameters = {};
        const domain = parameters.$domain ? parameters.$domain : this.domain;
        let path = '/api/EmployeeProfile/{id}';
        if (parameters.$path) {
            path = (typeof(parameters.$path) === 'function') ? parameters.$path(path) : parameters.$path;
        }

        path = path.replace(
            '{id}',
            `${encodeURIComponent(this.convertParameterCollectionFormat(
                        parameters['id'],
                        ''
                    ).toString())}`
        );

        if (parameters.$queryParameters) {
            queryParameters = {
                ...queryParameters,
                ...parameters.$queryParameters
            };
        }

        let keys = Object.keys(queryParameters);
        return domain + path + (keys.length > 0 ? '?' + (keys.map(key => key + '=' + encodeURIComponent(queryParameters[key])).join('&')) : '');
    }

    /**
     * 
     * @method
     * @name SwaggerCodegen#EmployeeProfile_GetEmployeeById
     * @param {string} id - 
     */
    EmployeeProfile_GetEmployeeById(parameters: {
        'id': string,
    } & CommonRequestOptions): Promise < ResponseWithBody < 200, EmployeeProfileDto > | ResponseWithBody < 404, ProblemDetails >> {
        const domain = parameters.$domain ? parameters.$domain : this.domain;
        let path = '/api/EmployeeProfile/{id}';
        if (parameters.$path) {
            path = (typeof(parameters.$path) === 'function') ? parameters.$path(path) : parameters.$path;
        }

        let body: any;
        let queryParameters: QueryParameters = {};
        let headers: RequestHeaders = {};
        let form: any = {};
        return new Promise((resolve, reject) => {
            headers['Accept'] = 'text/plain, application/json, text/json';

            path = path.replace(
                '{id}',
                `${encodeURIComponent(this.convertParameterCollectionFormat(
                    parameters['id'],
                    ''
                ).toString())}`
            );

            if (parameters['id'] === undefined) {
                reject(new Error('Missing required  parameter: id'));
                return;
            }

            if (parameters.$queryParameters) {
                queryParameters = {
                    ...queryParameters,
                    ...parameters.$queryParameters
                };
            }

            this.request('GET', domain + path, body, headers, queryParameters, form, reject, resolve, parameters);
        });
    }

    EmployeeProfile_UpdateURL(parameters: {
        'id': string,
        'employeeProfileDto': EmployeeProfileUpdateDto,
    } & CommonRequestOptions): string {
        let queryParameters: QueryParameters = {};
        const domain = parameters.$domain ? parameters.$domain : this.domain;
        let path = '/api/EmployeeProfile/{id}';
        if (parameters.$path) {
            path = (typeof(parameters.$path) === 'function') ? parameters.$path(path) : parameters.$path;
        }

        path = path.replace(
            '{id}',
            `${encodeURIComponent(this.convertParameterCollectionFormat(
                        parameters['id'],
                        ''
                    ).toString())}`
        );

        if (parameters.$queryParameters) {
            queryParameters = {
                ...queryParameters,
                ...parameters.$queryParameters
            };
        }

        let keys = Object.keys(queryParameters);
        return domain + path + (keys.length > 0 ? '?' + (keys.map(key => key + '=' + encodeURIComponent(queryParameters[key])).join('&')) : '');
    }

    /**
     * 
     * @method
     * @name SwaggerCodegen#EmployeeProfile_Update
     * @param {string} id - 
     * @param {} employeeProfileDto - 
     */
    EmployeeProfile_Update(parameters: {
        'id': string,
        'employeeProfileDto': EmployeeProfileUpdateDto,
    } & CommonRequestOptions): Promise < ResponseWithBody < 200, EmployeeProfileDto > | ResponseWithBody < 400, ProblemDetails > | ResponseWithBody < 401, ProblemDetails > | ResponseWithBody < 404, ProblemDetails >> {
        const domain = parameters.$domain ? parameters.$domain : this.domain;
        let path = '/api/EmployeeProfile/{id}';
        if (parameters.$path) {
            path = (typeof(parameters.$path) === 'function') ? parameters.$path(path) : parameters.$path;
        }

        let body: any;
        let queryParameters: QueryParameters = {};
        let headers: RequestHeaders = {};
        let form: any = {};
        return new Promise((resolve, reject) => {
            headers['Accept'] = 'text/plain, application/json, text/json';
            headers['Content-Type'] = 'application/json-patch+json';

            path = path.replace(
                '{id}',
                `${encodeURIComponent(this.convertParameterCollectionFormat(
                    parameters['id'],
                    ''
                ).toString())}`
            );

            if (parameters['id'] === undefined) {
                reject(new Error('Missing required  parameter: id'));
                return;
            }

            if (parameters['employeeProfileDto'] !== undefined) {
                body = parameters['employeeProfileDto'];
            }

            if (parameters['employeeProfileDto'] === undefined) {
                reject(new Error('Missing required  parameter: employeeProfileDto'));
                return;
            }

            if (parameters.$queryParameters) {
                queryParameters = {
                    ...queryParameters,
                    ...parameters.$queryParameters
                };
            }

            this.request('PUT', domain + path, body, headers, queryParameters, form, reject, resolve, parameters);
        });
    }

    EmployeeProfile_DeleteURL(parameters: {
        'id': string,
    } & CommonRequestOptions): string {
        let queryParameters: QueryParameters = {};
        const domain = parameters.$domain ? parameters.$domain : this.domain;
        let path = '/api/EmployeeProfile/{id}';
        if (parameters.$path) {
            path = (typeof(parameters.$path) === 'function') ? parameters.$path(path) : parameters.$path;
        }

        path = path.replace(
            '{id}',
            `${encodeURIComponent(this.convertParameterCollectionFormat(
                        parameters['id'],
                        ''
                    ).toString())}`
        );

        if (parameters.$queryParameters) {
            queryParameters = {
                ...queryParameters,
                ...parameters.$queryParameters
            };
        }

        let keys = Object.keys(queryParameters);
        return domain + path + (keys.length > 0 ? '?' + (keys.map(key => key + '=' + encodeURIComponent(queryParameters[key])).join('&')) : '');
    }

    /**
     * 
     * @method
     * @name SwaggerCodegen#EmployeeProfile_Delete
     * @param {string} id - 
     */
    EmployeeProfile_Delete(parameters: {
        'id': string,
    } & CommonRequestOptions): Promise < ResponseWithBody < 200, EmployeeProfileDto > | ResponseWithBody < 400, ProblemDetails > | ResponseWithBody < 401, ProblemDetails > | ResponseWithBody < 404, ProblemDetails >> {
        const domain = parameters.$domain ? parameters.$domain : this.domain;
        let path = '/api/EmployeeProfile/{id}';
        if (parameters.$path) {
            path = (typeof(parameters.$path) === 'function') ? parameters.$path(path) : parameters.$path;
        }

        let body: any;
        let queryParameters: QueryParameters = {};
        let headers: RequestHeaders = {};
        let form: any = {};
        return new Promise((resolve, reject) => {
            headers['Accept'] = 'text/plain, application/json, text/json';

            path = path.replace(
                '{id}',
                `${encodeURIComponent(this.convertParameterCollectionFormat(
                    parameters['id'],
                    ''
                ).toString())}`
            );

            if (parameters['id'] === undefined) {
                reject(new Error('Missing required  parameter: id'));
                return;
            }

            if (parameters.$queryParameters) {
                queryParameters = {
                    ...queryParameters,
                    ...parameters.$queryParameters
                };
            }

            this.request('DELETE', domain + path, body, headers, queryParameters, form, reject, resolve, parameters);
        });
    }

    EmployeeProfile_GetEmployeesURL(parameters: {
        'searchString' ? : string,
        'serviceTypeId' ? : number,
        'maxServiceCost' ? : number,
        'minAverageRate' ? : number,
        'pageSize' ? : number,
        'pageNumber' ? : number,
    } & CommonRequestOptions): string {
        let queryParameters: QueryParameters = {};
        const domain = parameters.$domain ? parameters.$domain : this.domain;
        let path = '/api/EmployeeProfile';
        if (parameters.$path) {
            path = (typeof(parameters.$path) === 'function') ? parameters.$path(path) : parameters.$path;
        }

        if (parameters['searchString'] !== undefined) {
            queryParameters['searchString'] = this.convertParameterCollectionFormat(
                parameters['searchString'],
                ''
            );
        }

        if (parameters['serviceTypeId'] !== undefined) {
            queryParameters['serviceTypeId'] = this.convertParameterCollectionFormat(
                parameters['serviceTypeId'],
                ''
            );
        }

        if (parameters['maxServiceCost'] !== undefined) {
            queryParameters['maxServiceCost'] = this.convertParameterCollectionFormat(
                parameters['maxServiceCost'],
                ''
            );
        }

        if (parameters['minAverageRate'] !== undefined) {
            queryParameters['minAverageRate'] = this.convertParameterCollectionFormat(
                parameters['minAverageRate'],
                ''
            );
        }

        if (parameters['pageSize'] !== undefined) {
            queryParameters['pageSize'] = this.convertParameterCollectionFormat(
                parameters['pageSize'],
                ''
            );
        }

        if (parameters['pageNumber'] !== undefined) {
            queryParameters['pageNumber'] = this.convertParameterCollectionFormat(
                parameters['pageNumber'],
                ''
            );
        }

        if (parameters.$queryParameters) {
            queryParameters = {
                ...queryParameters,
                ...parameters.$queryParameters
            };
        }

        let keys = Object.keys(queryParameters);
        return domain + path + (keys.length > 0 ? '?' + (keys.map(key => key + '=' + encodeURIComponent(queryParameters[key])).join('&')) : '');
    }

    /**
     * 
     * @method
     * @name SwaggerCodegen#EmployeeProfile_GetEmployees
     * @param {string} searchString - 
     * @param {integer} serviceTypeId - 
     * @param {number} maxServiceCost - 
     * @param {integer} minAverageRate - 
     * @param {integer} pageSize - 
     * @param {integer} pageNumber - 
     */
    EmployeeProfile_GetEmployees(parameters: {
        'searchString' ? : string,
        'serviceTypeId' ? : number,
        'maxServiceCost' ? : number,
        'minAverageRate' ? : number,
        'pageSize' ? : number,
        'pageNumber' ? : number,
    } & CommonRequestOptions): Promise < ResponseWithBody < 200, IPagedResultOfEmployeeProfileDto > | ResponseWithBody < 400, ProblemDetails >> {
        const domain = parameters.$domain ? parameters.$domain : this.domain;
        let path = '/api/EmployeeProfile';
        if (parameters.$path) {
            path = (typeof(parameters.$path) === 'function') ? parameters.$path(path) : parameters.$path;
        }

        let body: any;
        let queryParameters: QueryParameters = {};
        let headers: RequestHeaders = {};
        let form: any = {};
        return new Promise((resolve, reject) => {
            headers['Accept'] = 'text/plain, application/json, text/json';

            if (parameters['searchString'] !== undefined) {
                queryParameters['searchString'] = this.convertParameterCollectionFormat(
                    parameters['searchString'],
                    ''
                );
            }

            if (parameters['serviceTypeId'] !== undefined) {
                queryParameters['serviceTypeId'] = this.convertParameterCollectionFormat(
                    parameters['serviceTypeId'],
                    ''
                );
            }

            if (parameters['maxServiceCost'] !== undefined) {
                queryParameters['maxServiceCost'] = this.convertParameterCollectionFormat(
                    parameters['maxServiceCost'],
                    ''
                );
            }

            if (parameters['minAverageRate'] !== undefined) {
                queryParameters['minAverageRate'] = this.convertParameterCollectionFormat(
                    parameters['minAverageRate'],
                    ''
                );
            }

            if (parameters['pageSize'] !== undefined) {
                queryParameters['pageSize'] = this.convertParameterCollectionFormat(
                    parameters['pageSize'],
                    ''
                );
            }

            if (parameters['pageNumber'] !== undefined) {
                queryParameters['pageNumber'] = this.convertParameterCollectionFormat(
                    parameters['pageNumber'],
                    ''
                );
            }

            if (parameters.$queryParameters) {
                queryParameters = {
                    ...queryParameters,
                    ...parameters.$queryParameters
                };
            }

            this.request('GET', domain + path, body, headers, queryParameters, form, reject, resolve, parameters);
        });
    }

    EmployeeProfile_CreateURL(parameters: {
        'employeeProfileDto': EmployeeProfileCreateDto,
    } & CommonRequestOptions): string {
        let queryParameters: QueryParameters = {};
        const domain = parameters.$domain ? parameters.$domain : this.domain;
        let path = '/api/EmployeeProfile';
        if (parameters.$path) {
            path = (typeof(parameters.$path) === 'function') ? parameters.$path(path) : parameters.$path;
        }

        if (parameters.$queryParameters) {
            queryParameters = {
                ...queryParameters,
                ...parameters.$queryParameters
            };
        }

        queryParameters = {};

        let keys = Object.keys(queryParameters);
        return domain + path + (keys.length > 0 ? '?' + (keys.map(key => key + '=' + encodeURIComponent(queryParameters[key])).join('&')) : '');
    }

    /**
     * 
     * @method
     * @name SwaggerCodegen#EmployeeProfile_Create
     * @param {} employeeProfileDto - 
     */
    EmployeeProfile_Create(parameters: {
        'employeeProfileDto': EmployeeProfileCreateDto,
    } & CommonRequestOptions): Promise < ResponseWithBody < 200, EmployeeProfileDto > | ResponseWithBody < 400, ProblemDetails > | ResponseWithBody < 401, ProblemDetails >> {
        const domain = parameters.$domain ? parameters.$domain : this.domain;
        let path = '/api/EmployeeProfile';
        if (parameters.$path) {
            path = (typeof(parameters.$path) === 'function') ? parameters.$path(path) : parameters.$path;
        }

        let body: any;
        let queryParameters: QueryParameters = {};
        let headers: RequestHeaders = {};
        let form: any = {};
        return new Promise((resolve, reject) => {
            headers['Accept'] = 'text/plain, application/json, text/json';
            headers['Content-Type'] = 'application/json-patch+json';

            if (parameters['employeeProfileDto'] !== undefined) {
                body = parameters['employeeProfileDto'];
            }

            if (parameters['employeeProfileDto'] === undefined) {
                reject(new Error('Missing required  parameter: employeeProfileDto'));
                return;
            }

            if (parameters.$queryParameters) {
                queryParameters = {
                    ...queryParameters,
                    ...parameters.$queryParameters
                };
            }

            form = queryParameters;
            queryParameters = {};

            this.request('POST', domain + path, body, headers, queryParameters, form, reject, resolve, parameters);
        });
    }

    Order_GetUserOrdersURL(parameters: {
        'id': string,
        'pageSize' ? : number,
        'pageNumber' ? : number,
    } & CommonRequestOptions): string {
        let queryParameters: QueryParameters = {};
        const domain = parameters.$domain ? parameters.$domain : this.domain;
        let path = '/api/Order/user/{id}';
        if (parameters.$path) {
            path = (typeof(parameters.$path) === 'function') ? parameters.$path(path) : parameters.$path;
        }

        path = path.replace(
            '{id}',
            `${encodeURIComponent(this.convertParameterCollectionFormat(
                        parameters['id'],
                        ''
                    ).toString())}`
        );
        if (parameters['pageSize'] !== undefined) {
            queryParameters['pageSize'] = this.convertParameterCollectionFormat(
                parameters['pageSize'],
                ''
            );
        }

        if (parameters['pageNumber'] !== undefined) {
            queryParameters['pageNumber'] = this.convertParameterCollectionFormat(
                parameters['pageNumber'],
                ''
            );
        }

        if (parameters.$queryParameters) {
            queryParameters = {
                ...queryParameters,
                ...parameters.$queryParameters
            };
        }

        let keys = Object.keys(queryParameters);
        return domain + path + (keys.length > 0 ? '?' + (keys.map(key => key + '=' + encodeURIComponent(queryParameters[key])).join('&')) : '');
    }

    /**
     * 
     * @method
     * @name SwaggerCodegen#Order_GetUserOrders
     * @param {string} id - 
     * @param {integer} pageSize - 
     * @param {integer} pageNumber - 
     */
    Order_GetUserOrders(parameters: {
        'id': string,
        'pageSize' ? : number,
        'pageNumber' ? : number,
    } & CommonRequestOptions): Promise < ResponseWithBody < 200, IPagedResultOfOrderDto > | ResponseWithBody < 400, ProblemDetails > | ResponseWithBody < 401, ProblemDetails >> {
        const domain = parameters.$domain ? parameters.$domain : this.domain;
        let path = '/api/Order/user/{id}';
        if (parameters.$path) {
            path = (typeof(parameters.$path) === 'function') ? parameters.$path(path) : parameters.$path;
        }

        let body: any;
        let queryParameters: QueryParameters = {};
        let headers: RequestHeaders = {};
        let form: any = {};
        return new Promise((resolve, reject) => {
            headers['Accept'] = 'text/plain, application/json, text/json';

            path = path.replace(
                '{id}',
                `${encodeURIComponent(this.convertParameterCollectionFormat(
                    parameters['id'],
                    ''
                ).toString())}`
            );

            if (parameters['id'] === undefined) {
                reject(new Error('Missing required  parameter: id'));
                return;
            }

            if (parameters['pageSize'] !== undefined) {
                queryParameters['pageSize'] = this.convertParameterCollectionFormat(
                    parameters['pageSize'],
                    ''
                );
            }

            if (parameters['pageNumber'] !== undefined) {
                queryParameters['pageNumber'] = this.convertParameterCollectionFormat(
                    parameters['pageNumber'],
                    ''
                );
            }

            if (parameters.$queryParameters) {
                queryParameters = {
                    ...queryParameters,
                    ...parameters.$queryParameters
                };
            }

            this.request('GET', domain + path, body, headers, queryParameters, form, reject, resolve, parameters);
        });
    }

    Order_GetEmployeeOrdersURL(parameters: {
        'id': string,
        'pageSize' ? : number,
        'pageNumber' ? : number,
    } & CommonRequestOptions): string {
        let queryParameters: QueryParameters = {};
        const domain = parameters.$domain ? parameters.$domain : this.domain;
        let path = '/api/Order/employee/{id}';
        if (parameters.$path) {
            path = (typeof(parameters.$path) === 'function') ? parameters.$path(path) : parameters.$path;
        }

        path = path.replace(
            '{id}',
            `${encodeURIComponent(this.convertParameterCollectionFormat(
                        parameters['id'],
                        ''
                    ).toString())}`
        );
        if (parameters['pageSize'] !== undefined) {
            queryParameters['pageSize'] = this.convertParameterCollectionFormat(
                parameters['pageSize'],
                ''
            );
        }

        if (parameters['pageNumber'] !== undefined) {
            queryParameters['pageNumber'] = this.convertParameterCollectionFormat(
                parameters['pageNumber'],
                ''
            );
        }

        if (parameters.$queryParameters) {
            queryParameters = {
                ...queryParameters,
                ...parameters.$queryParameters
            };
        }

        let keys = Object.keys(queryParameters);
        return domain + path + (keys.length > 0 ? '?' + (keys.map(key => key + '=' + encodeURIComponent(queryParameters[key])).join('&')) : '');
    }

    /**
     * 
     * @method
     * @name SwaggerCodegen#Order_GetEmployeeOrders
     * @param {string} id - 
     * @param {integer} pageSize - 
     * @param {integer} pageNumber - 
     */
    Order_GetEmployeeOrders(parameters: {
        'id': string,
        'pageSize' ? : number,
        'pageNumber' ? : number,
    } & CommonRequestOptions): Promise < ResponseWithBody < 200, IPagedResultOfOrderDto > | ResponseWithBody < 400, ProblemDetails > | ResponseWithBody < 401, ProblemDetails >> {
        const domain = parameters.$domain ? parameters.$domain : this.domain;
        let path = '/api/Order/employee/{id}';
        if (parameters.$path) {
            path = (typeof(parameters.$path) === 'function') ? parameters.$path(path) : parameters.$path;
        }

        let body: any;
        let queryParameters: QueryParameters = {};
        let headers: RequestHeaders = {};
        let form: any = {};
        return new Promise((resolve, reject) => {
            headers['Accept'] = 'text/plain, application/json, text/json';

            path = path.replace(
                '{id}',
                `${encodeURIComponent(this.convertParameterCollectionFormat(
                    parameters['id'],
                    ''
                ).toString())}`
            );

            if (parameters['id'] === undefined) {
                reject(new Error('Missing required  parameter: id'));
                return;
            }

            if (parameters['pageSize'] !== undefined) {
                queryParameters['pageSize'] = this.convertParameterCollectionFormat(
                    parameters['pageSize'],
                    ''
                );
            }

            if (parameters['pageNumber'] !== undefined) {
                queryParameters['pageNumber'] = this.convertParameterCollectionFormat(
                    parameters['pageNumber'],
                    ''
                );
            }

            if (parameters.$queryParameters) {
                queryParameters = {
                    ...queryParameters,
                    ...parameters.$queryParameters
                };
            }

            this.request('GET', domain + path, body, headers, queryParameters, form, reject, resolve, parameters);
        });
    }

    Order_CreateURL(parameters: {
        'orderDto': OrderCreateDto,
    } & CommonRequestOptions): string {
        let queryParameters: QueryParameters = {};
        const domain = parameters.$domain ? parameters.$domain : this.domain;
        let path = '/api/Order';
        if (parameters.$path) {
            path = (typeof(parameters.$path) === 'function') ? parameters.$path(path) : parameters.$path;
        }

        if (parameters.$queryParameters) {
            queryParameters = {
                ...queryParameters,
                ...parameters.$queryParameters
            };
        }

        queryParameters = {};

        let keys = Object.keys(queryParameters);
        return domain + path + (keys.length > 0 ? '?' + (keys.map(key => key + '=' + encodeURIComponent(queryParameters[key])).join('&')) : '');
    }

    /**
     * 
     * @method
     * @name SwaggerCodegen#Order_Create
     * @param {} orderDto - 
     */
    Order_Create(parameters: {
        'orderDto': OrderCreateDto,
    } & CommonRequestOptions): Promise < ResponseWithBody < 200, OrderDto > | ResponseWithBody < 400, ProblemDetails > | ResponseWithBody < 401, ProblemDetails >> {
        const domain = parameters.$domain ? parameters.$domain : this.domain;
        let path = '/api/Order';
        if (parameters.$path) {
            path = (typeof(parameters.$path) === 'function') ? parameters.$path(path) : parameters.$path;
        }

        let body: any;
        let queryParameters: QueryParameters = {};
        let headers: RequestHeaders = {};
        let form: any = {};
        return new Promise((resolve, reject) => {
            headers['Accept'] = 'text/plain, application/json, text/json';
            headers['Content-Type'] = 'application/json-patch+json';

            if (parameters['orderDto'] !== undefined) {
                body = parameters['orderDto'];
            }

            if (parameters['orderDto'] === undefined) {
                reject(new Error('Missing required  parameter: orderDto'));
                return;
            }

            if (parameters.$queryParameters) {
                queryParameters = {
                    ...queryParameters,
                    ...parameters.$queryParameters
                };
            }

            form = queryParameters;
            queryParameters = {};

            this.request('POST', domain + path, body, headers, queryParameters, form, reject, resolve, parameters);
        });
    }

    Order_TakeURL(parameters: {
        'id': number,
    } & CommonRequestOptions): string {
        let queryParameters: QueryParameters = {};
        const domain = parameters.$domain ? parameters.$domain : this.domain;
        let path = '/api/Order/take/{id}';
        if (parameters.$path) {
            path = (typeof(parameters.$path) === 'function') ? parameters.$path(path) : parameters.$path;
        }

        path = path.replace(
            '{id}',
            `${encodeURIComponent(this.convertParameterCollectionFormat(
                        parameters['id'],
                        ''
                    ).toString())}`
        );

        if (parameters.$queryParameters) {
            queryParameters = {
                ...queryParameters,
                ...parameters.$queryParameters
            };
        }

        let keys = Object.keys(queryParameters);
        return domain + path + (keys.length > 0 ? '?' + (keys.map(key => key + '=' + encodeURIComponent(queryParameters[key])).join('&')) : '');
    }

    /**
     * 
     * @method
     * @name SwaggerCodegen#Order_Take
     * @param {integer} id - 
     */
    Order_Take(parameters: {
        'id': number,
    } & CommonRequestOptions): Promise < ResponseWithBody < 200, OrderDto > | ResponseWithBody < 400, ProblemDetails > | ResponseWithBody < 401, ProblemDetails > | ResponseWithBody < 404, ProblemDetails >> {
        const domain = parameters.$domain ? parameters.$domain : this.domain;
        let path = '/api/Order/take/{id}';
        if (parameters.$path) {
            path = (typeof(parameters.$path) === 'function') ? parameters.$path(path) : parameters.$path;
        }

        let body: any;
        let queryParameters: QueryParameters = {};
        let headers: RequestHeaders = {};
        let form: any = {};
        return new Promise((resolve, reject) => {
            headers['Accept'] = 'text/plain, application/json, text/json';

            path = path.replace(
                '{id}',
                `${encodeURIComponent(this.convertParameterCollectionFormat(
                    parameters['id'],
                    ''
                ).toString())}`
            );

            if (parameters['id'] === undefined) {
                reject(new Error('Missing required  parameter: id'));
                return;
            }

            if (parameters.$queryParameters) {
                queryParameters = {
                    ...queryParameters,
                    ...parameters.$queryParameters
                };
            }

            this.request('PUT', domain + path, body, headers, queryParameters, form, reject, resolve, parameters);
        });
    }

    Order_DeclineURL(parameters: {
        'id': number,
    } & CommonRequestOptions): string {
        let queryParameters: QueryParameters = {};
        const domain = parameters.$domain ? parameters.$domain : this.domain;
        let path = '/api/Order/decline/{id}';
        if (parameters.$path) {
            path = (typeof(parameters.$path) === 'function') ? parameters.$path(path) : parameters.$path;
        }

        path = path.replace(
            '{id}',
            `${encodeURIComponent(this.convertParameterCollectionFormat(
                        parameters['id'],
                        ''
                    ).toString())}`
        );

        if (parameters.$queryParameters) {
            queryParameters = {
                ...queryParameters,
                ...parameters.$queryParameters
            };
        }

        let keys = Object.keys(queryParameters);
        return domain + path + (keys.length > 0 ? '?' + (keys.map(key => key + '=' + encodeURIComponent(queryParameters[key])).join('&')) : '');
    }

    /**
     * 
     * @method
     * @name SwaggerCodegen#Order_Decline
     * @param {integer} id - 
     */
    Order_Decline(parameters: {
        'id': number,
    } & CommonRequestOptions): Promise < ResponseWithBody < 200, OrderDto > | ResponseWithBody < 400, ProblemDetails > | ResponseWithBody < 401, ProblemDetails > | ResponseWithBody < 404, ProblemDetails >> {
        const domain = parameters.$domain ? parameters.$domain : this.domain;
        let path = '/api/Order/decline/{id}';
        if (parameters.$path) {
            path = (typeof(parameters.$path) === 'function') ? parameters.$path(path) : parameters.$path;
        }

        let body: any;
        let queryParameters: QueryParameters = {};
        let headers: RequestHeaders = {};
        let form: any = {};
        return new Promise((resolve, reject) => {
            headers['Accept'] = 'text/plain, application/json, text/json';

            path = path.replace(
                '{id}',
                `${encodeURIComponent(this.convertParameterCollectionFormat(
                    parameters['id'],
                    ''
                ).toString())}`
            );

            if (parameters['id'] === undefined) {
                reject(new Error('Missing required  parameter: id'));
                return;
            }

            if (parameters.$queryParameters) {
                queryParameters = {
                    ...queryParameters,
                    ...parameters.$queryParameters
                };
            }

            this.request('PUT', domain + path, body, headers, queryParameters, form, reject, resolve, parameters);
        });
    }

    Order_ConfirmCompletionURL(parameters: {
        'id': number,
    } & CommonRequestOptions): string {
        let queryParameters: QueryParameters = {};
        const domain = parameters.$domain ? parameters.$domain : this.domain;
        let path = '/api/Order/confirm/{id}';
        if (parameters.$path) {
            path = (typeof(parameters.$path) === 'function') ? parameters.$path(path) : parameters.$path;
        }

        path = path.replace(
            '{id}',
            `${encodeURIComponent(this.convertParameterCollectionFormat(
                        parameters['id'],
                        ''
                    ).toString())}`
        );

        if (parameters.$queryParameters) {
            queryParameters = {
                ...queryParameters,
                ...parameters.$queryParameters
            };
        }

        let keys = Object.keys(queryParameters);
        return domain + path + (keys.length > 0 ? '?' + (keys.map(key => key + '=' + encodeURIComponent(queryParameters[key])).join('&')) : '');
    }

    /**
     * 
     * @method
     * @name SwaggerCodegen#Order_ConfirmCompletion
     * @param {integer} id - 
     */
    Order_ConfirmCompletion(parameters: {
        'id': number,
    } & CommonRequestOptions): Promise < ResponseWithBody < 200, OrderDto > | ResponseWithBody < 400, ProblemDetails > | ResponseWithBody < 401, ProblemDetails > | ResponseWithBody < 404, ProblemDetails >> {
        const domain = parameters.$domain ? parameters.$domain : this.domain;
        let path = '/api/Order/confirm/{id}';
        if (parameters.$path) {
            path = (typeof(parameters.$path) === 'function') ? parameters.$path(path) : parameters.$path;
        }

        let body: any;
        let queryParameters: QueryParameters = {};
        let headers: RequestHeaders = {};
        let form: any = {};
        return new Promise((resolve, reject) => {
            headers['Accept'] = 'text/plain, application/json, text/json';

            path = path.replace(
                '{id}',
                `${encodeURIComponent(this.convertParameterCollectionFormat(
                    parameters['id'],
                    ''
                ).toString())}`
            );

            if (parameters['id'] === undefined) {
                reject(new Error('Missing required  parameter: id'));
                return;
            }

            if (parameters.$queryParameters) {
                queryParameters = {
                    ...queryParameters,
                    ...parameters.$queryParameters
                };
            }

            this.request('PUT', domain + path, body, headers, queryParameters, form, reject, resolve, parameters);
        });
    }

    Order_DeleteURL(parameters: {
        'id': number,
    } & CommonRequestOptions): string {
        let queryParameters: QueryParameters = {};
        const domain = parameters.$domain ? parameters.$domain : this.domain;
        let path = '/api/Order/{id}';
        if (parameters.$path) {
            path = (typeof(parameters.$path) === 'function') ? parameters.$path(path) : parameters.$path;
        }

        path = path.replace(
            '{id}',
            `${encodeURIComponent(this.convertParameterCollectionFormat(
                        parameters['id'],
                        ''
                    ).toString())}`
        );

        if (parameters.$queryParameters) {
            queryParameters = {
                ...queryParameters,
                ...parameters.$queryParameters
            };
        }

        let keys = Object.keys(queryParameters);
        return domain + path + (keys.length > 0 ? '?' + (keys.map(key => key + '=' + encodeURIComponent(queryParameters[key])).join('&')) : '');
    }

    /**
     * 
     * @method
     * @name SwaggerCodegen#Order_Delete
     * @param {integer} id - 
     */
    Order_Delete(parameters: {
        'id': number,
    } & CommonRequestOptions): Promise < ResponseWithBody < 200, OrderDto > | ResponseWithBody < 401, ProblemDetails > | ResponseWithBody < 404, ProblemDetails >> {
        const domain = parameters.$domain ? parameters.$domain : this.domain;
        let path = '/api/Order/{id}';
        if (parameters.$path) {
            path = (typeof(parameters.$path) === 'function') ? parameters.$path(path) : parameters.$path;
        }

        let body: any;
        let queryParameters: QueryParameters = {};
        let headers: RequestHeaders = {};
        let form: any = {};
        return new Promise((resolve, reject) => {
            headers['Accept'] = 'text/plain, application/json, text/json';

            path = path.replace(
                '{id}',
                `${encodeURIComponent(this.convertParameterCollectionFormat(
                    parameters['id'],
                    ''
                ).toString())}`
            );

            if (parameters['id'] === undefined) {
                reject(new Error('Missing required  parameter: id'));
                return;
            }

            if (parameters.$queryParameters) {
                queryParameters = {
                    ...queryParameters,
                    ...parameters.$queryParameters
                };
            }

            this.request('DELETE', domain + path, body, headers, queryParameters, form, reject, resolve, parameters);
        });
    }

    Review_GetUserReviewsURL(parameters: {
        'id': string,
        'pageSize' ? : number,
        'pageNumber' ? : number,
    } & CommonRequestOptions): string {
        let queryParameters: QueryParameters = {};
        const domain = parameters.$domain ? parameters.$domain : this.domain;
        let path = '/api/Review/{id}';
        if (parameters.$path) {
            path = (typeof(parameters.$path) === 'function') ? parameters.$path(path) : parameters.$path;
        }

        path = path.replace(
            '{id}',
            `${encodeURIComponent(this.convertParameterCollectionFormat(
                        parameters['id'],
                        ''
                    ).toString())}`
        );
        if (parameters['pageSize'] !== undefined) {
            queryParameters['pageSize'] = this.convertParameterCollectionFormat(
                parameters['pageSize'],
                ''
            );
        }

        if (parameters['pageNumber'] !== undefined) {
            queryParameters['pageNumber'] = this.convertParameterCollectionFormat(
                parameters['pageNumber'],
                ''
            );
        }

        if (parameters.$queryParameters) {
            queryParameters = {
                ...queryParameters,
                ...parameters.$queryParameters
            };
        }

        let keys = Object.keys(queryParameters);
        return domain + path + (keys.length > 0 ? '?' + (keys.map(key => key + '=' + encodeURIComponent(queryParameters[key])).join('&')) : '');
    }

    /**
     * 
     * @method
     * @name SwaggerCodegen#Review_GetUserReviews
     * @param {string} id - 
     * @param {integer} pageSize - 
     * @param {integer} pageNumber - 
     */
    Review_GetUserReviews(parameters: {
        'id': string,
        'pageSize' ? : number,
        'pageNumber' ? : number,
    } & CommonRequestOptions): Promise < ResponseWithBody < 200, IPagedResultOfReviewDto > | ResponseWithBody < 400, ProblemDetails >> {
        const domain = parameters.$domain ? parameters.$domain : this.domain;
        let path = '/api/Review/{id}';
        if (parameters.$path) {
            path = (typeof(parameters.$path) === 'function') ? parameters.$path(path) : parameters.$path;
        }

        let body: any;
        let queryParameters: QueryParameters = {};
        let headers: RequestHeaders = {};
        let form: any = {};
        return new Promise((resolve, reject) => {
            headers['Accept'] = 'text/plain, application/json, text/json';

            path = path.replace(
                '{id}',
                `${encodeURIComponent(this.convertParameterCollectionFormat(
                    parameters['id'],
                    ''
                ).toString())}`
            );

            if (parameters['id'] === undefined) {
                reject(new Error('Missing required  parameter: id'));
                return;
            }

            if (parameters['pageSize'] !== undefined) {
                queryParameters['pageSize'] = this.convertParameterCollectionFormat(
                    parameters['pageSize'],
                    ''
                );
            }

            if (parameters['pageNumber'] !== undefined) {
                queryParameters['pageNumber'] = this.convertParameterCollectionFormat(
                    parameters['pageNumber'],
                    ''
                );
            }

            if (parameters.$queryParameters) {
                queryParameters = {
                    ...queryParameters,
                    ...parameters.$queryParameters
                };
            }

            this.request('GET', domain + path, body, headers, queryParameters, form, reject, resolve, parameters);
        });
    }

    Review_DeleteURL(parameters: {
        'id': number,
    } & CommonRequestOptions): string {
        let queryParameters: QueryParameters = {};
        const domain = parameters.$domain ? parameters.$domain : this.domain;
        let path = '/api/Review/{id}';
        if (parameters.$path) {
            path = (typeof(parameters.$path) === 'function') ? parameters.$path(path) : parameters.$path;
        }

        path = path.replace(
            '{id}',
            `${encodeURIComponent(this.convertParameterCollectionFormat(
                        parameters['id'],
                        ''
                    ).toString())}`
        );

        if (parameters.$queryParameters) {
            queryParameters = {
                ...queryParameters,
                ...parameters.$queryParameters
            };
        }

        let keys = Object.keys(queryParameters);
        return domain + path + (keys.length > 0 ? '?' + (keys.map(key => key + '=' + encodeURIComponent(queryParameters[key])).join('&')) : '');
    }

    /**
     * 
     * @method
     * @name SwaggerCodegen#Review_Delete
     * @param {integer} id - 
     */
    Review_Delete(parameters: {
        'id': number,
    } & CommonRequestOptions): Promise < ResponseWithBody < 200, ReviewDto > | ResponseWithBody < 404, ProblemDetails >> {
        const domain = parameters.$domain ? parameters.$domain : this.domain;
        let path = '/api/Review/{id}';
        if (parameters.$path) {
            path = (typeof(parameters.$path) === 'function') ? parameters.$path(path) : parameters.$path;
        }

        let body: any;
        let queryParameters: QueryParameters = {};
        let headers: RequestHeaders = {};
        let form: any = {};
        return new Promise((resolve, reject) => {
            headers['Accept'] = 'text/plain, application/json, text/json';

            path = path.replace(
                '{id}',
                `${encodeURIComponent(this.convertParameterCollectionFormat(
                    parameters['id'],
                    ''
                ).toString())}`
            );

            if (parameters['id'] === undefined) {
                reject(new Error('Missing required  parameter: id'));
                return;
            }

            if (parameters.$queryParameters) {
                queryParameters = {
                    ...queryParameters,
                    ...parameters.$queryParameters
                };
            }

            this.request('DELETE', domain + path, body, headers, queryParameters, form, reject, resolve, parameters);
        });
    }

    Review_CreateURL(parameters: {
        'reviewDto': ReviewCreateDto,
    } & CommonRequestOptions): string {
        let queryParameters: QueryParameters = {};
        const domain = parameters.$domain ? parameters.$domain : this.domain;
        let path = '/api/Review';
        if (parameters.$path) {
            path = (typeof(parameters.$path) === 'function') ? parameters.$path(path) : parameters.$path;
        }

        if (parameters.$queryParameters) {
            queryParameters = {
                ...queryParameters,
                ...parameters.$queryParameters
            };
        }

        queryParameters = {};

        let keys = Object.keys(queryParameters);
        return domain + path + (keys.length > 0 ? '?' + (keys.map(key => key + '=' + encodeURIComponent(queryParameters[key])).join('&')) : '');
    }

    /**
     * 
     * @method
     * @name SwaggerCodegen#Review_Create
     * @param {} reviewDto - 
     */
    Review_Create(parameters: {
        'reviewDto': ReviewCreateDto,
    } & CommonRequestOptions): Promise < ResponseWithBody < 200, ReviewDto > | ResponseWithBody < 400, ProblemDetails > | ResponseWithBody < 401, ProblemDetails >> {
        const domain = parameters.$domain ? parameters.$domain : this.domain;
        let path = '/api/Review';
        if (parameters.$path) {
            path = (typeof(parameters.$path) === 'function') ? parameters.$path(path) : parameters.$path;
        }

        let body: any;
        let queryParameters: QueryParameters = {};
        let headers: RequestHeaders = {};
        let form: any = {};
        return new Promise((resolve, reject) => {
            headers['Accept'] = 'text/plain, application/json, text/json';
            headers['Content-Type'] = 'application/json-patch+json';

            if (parameters['reviewDto'] !== undefined) {
                body = parameters['reviewDto'];
            }

            if (parameters['reviewDto'] === undefined) {
                reject(new Error('Missing required  parameter: reviewDto'));
                return;
            }

            if (parameters.$queryParameters) {
                queryParameters = {
                    ...queryParameters,
                    ...parameters.$queryParameters
                };
            }

            form = queryParameters;
            queryParameters = {};

            this.request('POST', domain + path, body, headers, queryParameters, form, reject, resolve, parameters);
        });
    }

    ServiceType_GetAllOrderedByProfilesCountURL(parameters: {} & CommonRequestOptions): string {
        let queryParameters: QueryParameters = {};
        const domain = parameters.$domain ? parameters.$domain : this.domain;
        let path = '/api/ServiceType';
        if (parameters.$path) {
            path = (typeof(parameters.$path) === 'function') ? parameters.$path(path) : parameters.$path;
        }

        if (parameters.$queryParameters) {
            queryParameters = {
                ...queryParameters,
                ...parameters.$queryParameters
            };
        }

        let keys = Object.keys(queryParameters);
        return domain + path + (keys.length > 0 ? '?' + (keys.map(key => key + '=' + encodeURIComponent(queryParameters[key])).join('&')) : '');
    }

    /**
     * 
     * @method
     * @name SwaggerCodegen#ServiceType_GetAllOrderedByProfilesCount
     */
    ServiceType_GetAllOrderedByProfilesCount(parameters: {} & CommonRequestOptions): Promise < ResponseWithBody < 200, Response_ServiceType_GetAllOrderedByProfilesCount_200 >> {
        const domain = parameters.$domain ? parameters.$domain : this.domain;
        let path = '/api/ServiceType';
        if (parameters.$path) {
            path = (typeof(parameters.$path) === 'function') ? parameters.$path(path) : parameters.$path;
        }

        let body: any;
        let queryParameters: QueryParameters = {};
        let headers: RequestHeaders = {};
        let form: any = {};
        return new Promise((resolve, reject) => {
            headers['Accept'] = 'text/plain, application/json, text/json';

            if (parameters.$queryParameters) {
                queryParameters = {
                    ...queryParameters,
                    ...parameters.$queryParameters
                };
            }

            this.request('GET', domain + path, body, headers, queryParameters, form, reject, resolve, parameters);
        });
    }

}

export default SwaggerCodegen;