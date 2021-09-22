import { Component, OnInit, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ItemService } from '../../services/item.service';
import { HttpProgressEvent, HttpEventType, HttpResponse } from '@angular/common/http'
import { ItemForCreation, ITEM_TYPE } from '../../models/item'
import { FormGroup, FormControl, Validators } from '@angular/forms';

export interface AddItemDialogData {
  name: string;
}

@Component({
  selector: 'app-add-item-dialog',
  templateUrl: './add-item-dialog.component.html',
  styleUrls: ['./add-item-dialog.component.scss']
})
export class AddItemDialogComponent implements OnInit {

  file: File | null = null;
  percentDone = 0;
  uploadSuccess = false;
  errorMessage = '';

  public addFileItemGroup!: FormGroup;

  constructor(
    public dialogRef: MatDialogRef<AddItemDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: AddItemDialogData,
    private itemService: ItemService) { }

  ngOnInit(): void {
    this.addFileItemGroup = new FormGroup({
      name : new FormControl('', [Validators.required])
    });
  }

  checkError(controlName: string, errorName: string): boolean  {
    return this.addFileItemGroup.controls[controlName].hasError(errorName);
  }

  onCancelClick(): void {
    this.dialogRef.close();
  }

  onOkClick(): void {

    this.addFileItemGroup.updateValueAndValidity();

    if(!this.addFileItemGroup.valid) {
      this.errorMessage = '入力に誤りがあります';
      return;
    }

    this.data.name = this.addFileItemGroup.get('name')?.value

    if(this.file === null) {
      this.errorMessage = 'ファイルを選択して下さい';
      return;
    }

    if(this.data.name === undefined || this.data.name === null) {
      this.errorMessage = 'ファイルアイテム名を入力してください';
      return;
    }


    const formData = new FormData();

    const item: ItemForCreation = {
      itemType: ITEM_TYPE.FILE,
      name: this.data.name
    };

    formData.append("json", JSON.stringify(item));

    formData.append("file", this.file);

    this.percentDone = 0;
    this.uploadSuccess = false;
    this.errorMessage = '';

    this.itemService.addAndUploadItem(formData)
      .subscribe((event: HttpProgressEvent) => {
        if (event.type === HttpEventType.UploadProgress) {
          if(event.total) {
            this.percentDone = Math.round(100 * event.loaded / event.total);
          }
        } else if (event instanceof HttpResponse) {
          this.uploadSuccess = true;

          this.dialogRef.close(this.data);
        }
      }, (error) => {
        if(error?.error?.errors) {
          // maybe this massage is comming from Aspnet Core
          this.errorMessage += Object.values(error.error.errors).reduce((accumulator, currentValue) => {
            var messageLines = (currentValue as string[]).reduce((acc,cur)=>{
              return acc + cur + "\n";
            }, "")
            var ret = (accumulator as string) + messageLines
            return ret;
          }, "")
        }

        if(error?.message) {
          this.errorMessage += error?.message + '\n';
        }

      });


    // this.dialogRef.close(this.data);
  }

  onFileChange($event: Event): void {
    const element = $event.currentTarget as HTMLInputElement;
    let fileList: FileList | null = element.files;
    if (fileList) {

      this.file = fileList[0];

      console.log("FileUpload -> files", fileList);


    }
  }

  isNameEmpty(): boolean {
    return this.data.name === undefined ||
        this.data.name === null ||
        this.data.name === ""
  }
}
