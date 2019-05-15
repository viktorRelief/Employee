import { NgModule } from '@angular/core';  
import { EmployeeService } from './services/empservice.service'
import { DepartmentService } from './services/depservice.service'
import { CommonModule } from '@angular/common';  
import { FormsModule, ReactiveFormsModule } from '@angular/forms';  
import { HttpModule } from '@angular/http';  
import { RouterModule } from '@angular/router';  
  
import { AppComponent } from './components/app/app.component';  
import { NavMenuComponent } from './components/navmenu/navmenu.component';  
import { HomeComponent } from './components/home/home.component';  
import { FetchEmployeeComponent } from './components/fetchemployee/fetchemployee.component'
import { AddDepartmenComponent } from './components/adddepartment/adddepartment.component'
import { createemployee } from './components/addemployee/AddEmployee.component'  
  
@NgModule({  
    declarations: [  
        AppComponent,  
        NavMenuComponent,  
        HomeComponent,  
        FetchEmployeeComponent,
        AddDepartmenComponent,
        createemployee,  
    ],  
    imports: [  
        CommonModule,  
        HttpModule,  
        FormsModule,  
        ReactiveFormsModule,  
        RouterModule.forRoot([  
            { path: '', redirectTo: 'home', pathMatch: 'full' },  
            { path: 'home', component: HomeComponent },  
            { path: 'fetch-employee', component: FetchEmployeeComponent },
            { path: 'add-department', component: AddDepartmenComponent },
            { path: 'register-employee', component: createemployee },
            { path: 'employee/edit/:id', component: createemployee },  
            { path: '**', redirectTo: 'home' }  
        ])  
    ],  
    providers: [EmployeeService, DepartmentService]  
})  
export class AppModuleShared {  
}  