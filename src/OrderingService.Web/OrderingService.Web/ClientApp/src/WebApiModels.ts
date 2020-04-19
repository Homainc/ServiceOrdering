// tslint:disable
import { userService } from './_services';
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

export type UserDTO = {
    'id' ? : string;
    'email' ? : string;
    'phoneNumber' ? : string;
    'password' ? : string;
    'role' ? : string;
    'firstName' ? : string;
    'lastName' ? : string;
    'token' ? : string;
    'imageUrl' ? : string;
    'employeeProfile' ? : EmployeeProfileDTO;
} & {
    [key: string]: any;
};

export type EmployeeProfileDTO = {
    'id': string;
    'serviceType' ? : string;
    'serviceCost': number;
    'description' ? : string;
    'userId': string;
    'user' ? : UserDTO;
    'averageRate' ? : number;
    'reviewsCount': number;
} & {
    [key: string]: any;
};

export type OrderDTO = {
    'id': number;
    'clientId': string;
    'client' ? : UserDTO;
    'employeeId': string;
    'employee' ? : EmployeeProfileDTO;
    'briefTask' ? : string;
    'serviceDetails' ? : string;
    'address' ? : string;
    'contactPhone' ? : string;
    'price': number;
    'date': string;
    'status': OrderStatus;
} & {
    [key: string]: any;
};

export type OrderStatus = 0 | 1 | 2 | 3;

export type ReviewDTO = {
    'id': number;
    'text' ? : string;
    'rate': number;
    'date': string;
    'clientId': string;
    'client' ? : UserDTO;
    'employeeId': string;
    'employee' ? : EmployeeProfileDTO;
} & {
    [key: string]: any;
};

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

        if ((opts.$timeout && opts.$timeout > 0) || (opts.$deadline && opts.$deadline > 0)) {
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
                
                // Custom error handling
                if (response.status === 401) {
                    userService.logout();
                    window.location.reload(true);
                }
                const errorMessage = response.body && (response.body.errorMessage || response.body.errors);

                reject(errorMessage);
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
        'userDto': UserDTO,
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
     * @param {} userDto - 
     */
    Account_Auth(parameters: {
        'userDto': UserDTO,
    } & CommonRequestOptions): Promise < ResponseWithBody < 200, void > | ResponseWithBody < 400, ProblemDetails >> {
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

    Account_SignUpURL(parameters: {
        'userDto': UserDTO,
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
        'userDto': UserDTO,
    } & CommonRequestOptions): Promise < ResponseWithBody < 200, void > | ResponseWithBody < 400, ProblemDetails >> {
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
    Account_GetProfile(parameters: {} & CommonRequestOptions): Promise < ResponseWithBody < 200, void > | ResponseWithBody < 400, ProblemDetails > | ResponseWithBody < 401, ProblemDetails >> {
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
        'userDto': UserDTO,
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
        'userDto': UserDTO,
    } & CommonRequestOptions): Promise < ResponseWithBody < 200, void > | ResponseWithBody < 400, ProblemDetails > | ResponseWithBody < 401, ProblemDetails >> {
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
    } & CommonRequestOptions): Promise < ResponseWithBody < 200, void > | ResponseWithBody < 404, ProblemDetails >> {
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
        'employeeProfileDto': EmployeeProfileDTO,
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
        'employeeProfileDto': EmployeeProfileDTO,
    } & CommonRequestOptions): Promise < ResponseWithBody < 200, void > | ResponseWithBody < 400, ProblemDetails > | ResponseWithBody < 401, ProblemDetails > | ResponseWithBody < 404, ProblemDetails >> {
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
    } & CommonRequestOptions): Promise < ResponseWithBody < 200, void > | ResponseWithBody < 400, ProblemDetails > | ResponseWithBody < 401, ProblemDetails > | ResponseWithBody < 404, ProblemDetails >> {
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
        'serviceName' ? : string,
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

        if (parameters['serviceName'] !== undefined) {
            queryParameters['serviceName'] = this.convertParameterCollectionFormat(
                parameters['serviceName'],
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
     * @param {string} serviceName - 
     * @param {number} maxServiceCost - 
     * @param {integer} minAverageRate - 
     * @param {integer} pageSize - 
     * @param {integer} pageNumber - 
     */
    EmployeeProfile_GetEmployees(parameters: {
        'serviceName' ? : string,
        'maxServiceCost' ? : number,
        'minAverageRate' ? : number,
        'pageSize' ? : number,
        'pageNumber' ? : number,
    } & CommonRequestOptions): Promise < ResponseWithBody < 200, void > | ResponseWithBody < 400, ProblemDetails >> {
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

            if (parameters['serviceName'] !== undefined) {
                queryParameters['serviceName'] = this.convertParameterCollectionFormat(
                    parameters['serviceName'],
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
        'employeeProfileDto': EmployeeProfileDTO,
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
        'employeeProfileDto': EmployeeProfileDTO,
    } & CommonRequestOptions): Promise < ResponseWithBody < 200, void > | ResponseWithBody < 400, ProblemDetails > | ResponseWithBody < 401, ProblemDetails >> {
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
    } & CommonRequestOptions): Promise < ResponseWithBody < 200, void > | ResponseWithBody < 400, ProblemDetails > | ResponseWithBody < 401, ProblemDetails >> {
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
    } & CommonRequestOptions): Promise < ResponseWithBody < 200, void > | ResponseWithBody < 400, ProblemDetails > | ResponseWithBody < 401, ProblemDetails >> {
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
        'orderDto': OrderDTO,
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
        'orderDto': OrderDTO,
    } & CommonRequestOptions): Promise < ResponseWithBody < 200, void > | ResponseWithBody < 400, ProblemDetails > | ResponseWithBody < 401, ProblemDetails >> {
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
    } & CommonRequestOptions): Promise < ResponseWithBody < 200, void > | ResponseWithBody < 400, ProblemDetails > | ResponseWithBody < 401, ProblemDetails > | ResponseWithBody < 404, ProblemDetails >> {
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
    } & CommonRequestOptions): Promise < ResponseWithBody < 200, void > | ResponseWithBody < 400, ProblemDetails > | ResponseWithBody < 401, ProblemDetails > | ResponseWithBody < 404, ProblemDetails >> {
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
    } & CommonRequestOptions): Promise < ResponseWithBody < 200, void > | ResponseWithBody < 400, ProblemDetails > | ResponseWithBody < 401, ProblemDetails > | ResponseWithBody < 404, ProblemDetails >> {
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
    } & CommonRequestOptions): Promise < ResponseWithBody < 200, void > | ResponseWithBody < 401, ProblemDetails > | ResponseWithBody < 404, ProblemDetails >> {
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
    } & CommonRequestOptions): Promise < ResponseWithBody < 200, void > | ResponseWithBody < 400, ProblemDetails >> {
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
    } & CommonRequestOptions): Promise < ResponseWithBody < 200, void > | ResponseWithBody < 404, ProblemDetails >> {
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
        'reviewDto': ReviewDTO,
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
        'reviewDto': ReviewDTO,
    } & CommonRequestOptions): Promise < ResponseWithBody < 200, void > | ResponseWithBody < 400, ProblemDetails > | ResponseWithBody < 401, ProblemDetails >> {
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

}

export default SwaggerCodegen;