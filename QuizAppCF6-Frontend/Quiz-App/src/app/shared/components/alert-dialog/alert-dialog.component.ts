import { Component, Inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialogModule } from '@angular/material/dialog';

@Component({
  selector: 'app-alert-dialog',
  standalone: true,
  imports: [MatDialogModule, MatButtonModule],
  templateUrl: './alert-dialog.component.html',
  styleUrls: ['./alert-dialog.component.css'],
})
export class AlertDialogComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public data: { title: string; message: string }) {}
}
