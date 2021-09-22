import { Component, OnInit } from '@angular/core';
import { Item, ItemForCreation, ITEM_TYPE } from '../../models/item'

import { ItemService } from '../../services/item.service'
import { MatDialog } from '@angular/material/dialog';
import { AddItemDialogComponent, AddItemDialogData } from '../add-item-dialog/add-item-dialog.component'

@Component({
  selector: 'app-explorer-view',
  templateUrl: './explorer-view.component.html',
  styleUrls: ['./explorer-view.component.scss']
})
export class ExplorerViewComponent implements OnInit {

  // make it accessible from template
  ITEM_TYPE = ITEM_TYPE

  items: Item[] = [
  ]

  displayedColumns: string[] = [ 'type', 'name', 'action' ];



  constructor(
    private itemService: ItemService,
    private dialog: MatDialog
  ) { }

  ngOnInit(): void {

    this.reload();

  }

  isFolder(value: ITEM_TYPE): boolean {
    return value === ITEM_TYPE.FOLDER
  }

  getItems(): void {
    this.itemService.getItems()
    .subscribe((items: Item[]) => this.items = items);
  }

  reload(): void {
    this.getItems();
  }

  onReload(): void {
    this.reload();
  }



  name = ''
  openDialog(): void {
    const data: AddItemDialogData = {name: ''}
    const dialogRef = this.dialog.open(AddItemDialogComponent, {
      width: '400px',
      data: data,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe((result: AddItemDialogData) => {
      console.log('The dialog was closed');
      if( result === undefined || result === null) {
        // cancelled

      } else {
        // closed with ok
        console.log('add item done.')

        // reload
        this.reload();

      }
    });
  }

  onAdd(): void {
    this.openDialog()
  }

  onDelete(data: Item): void {

    this.itemService.deleteItem(data.id)
    .subscribe(() => {
      console.log('delete item done.')

      // reload
      this.reload();
    });

  }
}
