import { Injectable, Inject } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Router } from '@angular/router';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

@Injectable()
export class EmployeeService {
    myAppUrl: string = "";

    constructor(private _http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.myAppUrl = baseUrl;
    }

    getDepartmentList() {
        return this._http.get(this.myAppUrl + 'api/Employee/GetDepartmentList')
            .map(res => res.json())
            .catch(this.errorHandler);
    }

    getEmployees() {
        return this._http.get(this.myAppUrl + 'api/Employee/Index')
            .map((response: Response) => response.json())
            .catch(this.errorHandler);
    }

    getEmployeeById(id: number) {
        return this._http.get(this.myAppUrl + "api/Employee/Details/" + id)
            .map((response: Response) => response.json())
            .catch(this.errorHandler)
    }

    saveEmployee(employee) {
        return this._http.post(this.myAppUrl + 'api/Employee/Create', employee)
            .catch(this.errorHandler)
    }

    updateEmployee(employee) {
        return this._http.put(this.myAppUrl + 'api/Employee/Edit', employee)
            .catch(this.errorHandler);
    }

    deleteEmployee(id) {
        return this._http.delete(this.myAppUrl + "api/Employee/Delete/" + id)
            .catch(this.errorHandler);
    }

    confirmDeleteEmployee(id: number) {
        return this._http.get(this.myAppUrl + "api/Employee/Delete/" + id)
            .catch(this.errorHandler);
    }

    addDepartment(department) {
        return this._http.post(this.myAppUrl + 'api/Department/AddDepartment', department)
            .map(res => res.json())
            .catch(this.errorHandler)
    }

    errorHandler(error: Response) {
        console.log(error);
        return Observable.throw(error);
    }
}  