import { Injectable } from '@angular/core';
import Swal from 'sweetalert2';

@Injectable({
 providedIn: 'root'
})
export class AlertService {

 success(message: string, title: string = 'Success') {
   return Swal.fire({
     icon: 'success',
     title: title,
     text: message,
     confirmButtonColor: '#3085d6'
   });
 }

 error(message: string, title: string = 'Error') {
   return Swal.fire({
     icon: 'error', 
     title: title,
     text: message,
     confirmButtonColor: '#d33'
   });
 }

 warning(message: string, title: string = 'Warning') {
   return Swal.fire({
     icon: 'warning',
     title: title, 
     text: message,
     confirmButtonColor: '#f8bb86'
   });
 }

 confirm(message: string, title: string = 'Are you sure?') {
   return Swal.fire({
     title: title,
     text: message,
     icon: 'question',
     showCancelButton: true,
     confirmButtonText: 'Yes',
     cancelButtonText: 'No',
     confirmButtonColor: '#3085d6',
     cancelButtonColor: '#d33'
   });
 }
}