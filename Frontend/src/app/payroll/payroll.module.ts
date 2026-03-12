import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { PayrollComponent } from "./payroll.component";
import { PayrollGenerateComponent } from './payroll-generate/payroll-generate.component';
import { CommonModule } from "@angular/common";
import { ReactiveFormsModule } from "@angular/forms";
import { OverlayModule } from "@angular/cdk/overlay";
const routes = [
    { path:'',component:PayrollComponent }
]
@NgModule({
    declarations:[PayrollComponent, PayrollGenerateComponent],
    imports:[RouterModule.forChild(routes),CommonModule,ReactiveFormsModule,OverlayModule]

})
export class PayrollModule{

}