import { Component, OnDestroy, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ButtonsModule } from '@progress/kendo-angular-buttons';
import { DateInputsModule } from '@progress/kendo-angular-dateinputs';
import { DialogsModule } from '@progress/kendo-angular-dialog';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import {
  DataStateChangeEvent,
  GridDataResult,
  GridModule,
} from '@progress/kendo-angular-grid';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { NotificationModule } from '@progress/kendo-angular-notification';
import { SortDescriptor } from '@progress/kendo-data-query';
import { Subscription } from 'rxjs';
import { GridRequestParamters } from '../../models/base/grid-request';
import { SaveAction } from '../../models/base/saveaction';
import { Client } from '../../models/client';
import { ClientService } from '../../services/clients/client.service';
import { ErrorHandelingService } from '../../services/error-handler/error-handeling.service';
import { CustomNotificationService } from '../../services/notification/notification.service';

@Component({
  selector: 'app-clients',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    GridModule,
    InputsModule,
    DropDownsModule,
    DateInputsModule,
    DialogsModule,
    ButtonsModule,
    NotificationModule,
  ],
  providers: [],
  templateUrl: './clients.component.html',
  styleUrl: './clients.component.scss',
})
export class ClientsComponent implements OnInit, OnDestroy {
  gridData: GridDataResult = { data: [], total: 0 };

  gridRequestParams: GridRequestParamters = {
    skip: 0,
    take: 5,
    sort: '',
    orderBy: 'Id',
    orderEnum: 'asc',
    filter: '',
    total: 0,
  };

  showModel = false;
  loading: boolean = true;
  btnLoading: boolean = false;
  formGroup?: FormGroup;
  isNew?: boolean;
  deleteClient?: Client;
  subscription: Subscription[] = [];
  sort: SortDescriptor[] = [
    {
      field: 'id',
      dir: 'asc',
    },
  ];

  constructor(
    private clientService: ClientService,
    private fb: FormBuilder,
    private notificationService: CustomNotificationService,
    public errorHandeling: ErrorHandelingService
  ) {}

  ngOnInit(): void {
    this.loadGridData();
    this.subscription.push(
      this.errorHandeling.emitError.subscribe((res) => {
        this.btnLoading = false;
        this.loading = false;
      })
    );
  }

  onSearch(value: string): void {
    this.gridRequestParams.filter = value;
    this.loadGridData();
  }

  loadGridData() {
    this.loading = true;
    this.subscription.push(
      this.clientService
        .getAll<GridDataResult>(this.gridRequestParams)
        .subscribe((res) => {
          this.gridData = res;
          this.loading = false;
        })
    );
  }
  dataStateChange(event: DataStateChangeEvent) {
    if (event.sort) {
      this.sort = event.sort;
      this.gridRequestParams.orderEnum = event.sort[0].dir ?? 'asc';
      this.gridRequestParams.orderBy = event.sort[0].field ?? 'Id';
    }
    this.gridRequestParams.skip = event.skip;
    this.gridRequestParams.take = event.take;
    this.loadGridData();
  }

  editHandler(client: Client): void {
    this.isNew = false;
    this.addFormGroup(client);
  }

  removeHandler(client: Client): void {
    this.deleteClient = client;
  }

  openCreateModal(): void {
    this.isNew = true;
    this.addFormGroup();
  }

  addFormGroup(client?: Client) {
    this.formGroup = this.fb.group({
      id: [client?.id ?? 0],
      firstName: [client?.firstName, Validators.required],
      lastName: [client?.lastName, Validators.required],
      email: [client?.email, [Validators.required, Validators.email]],
      phoneNumber: [
        client?.phoneNumber,
        [
          Validators.required,
          Validators.pattern(
            /^[+]?[0-9]{1,4}?[-.\s]?[(]?[0-9]{1,3}?[)]?[-.\s]?[0-9]{1,4}[-.\s]?[0-9]{1,4}[-.\s]?[0-9]{1,9}$/im
          ),
        ],
      ],
    });
  }

  onSubmit(): void {
    this.formGroup?.markAllAsTouched();
    if (this.formGroup!.valid) {
      this.btnLoading = true;
      const formValue = this.formGroup!.value;
      if (this.isNew) {
        this.subscription.push(
          this.clientService.create<Client>(formValue).subscribe({
            next: (res: SaveAction) => {
              this.handelSaveAction(
                res,
                () => {
                  this.btnLoading = false;
                },
                () => {
                  this.closeModal();
                  this.loadGridData();
                }
              );
            },
          })
        );
      } else {
        this.subscription.push(
          this.clientService
            .update<Client>(formValue)
            .subscribe((res: SaveAction) => {
              this.handelSaveAction(
                res,
                () => {
                  this.btnLoading = false;
                },
                () => {
                  this.closeModal();
                  this.loadGridData();
                }
              );
            })
        );
      }
    }
  }

  confirmDeleteClient(): void {
    this.loading = true;
    this.subscription.push(
      this.clientService.delete(this.deleteClient!.id).subscribe((res) => {
        this.handelSaveAction(res, () => {
          this.btnLoading = false;
          this.deleteClient = undefined;
        });
      })
    );
  }

  handelSaveAction(
    res: SaveAction,
    completeCallBack?: Function,
    successCallBack?: Function
  ) {
    if (res.success) {
      this.notificationService.showSuccess(res.message);

      this.loadGridData();
      if (successCallBack) {
        successCallBack();
      }
    } else {
      this.notificationService.showError(res.message);
    }
    if (completeCallBack) {
      completeCallBack();
    }
  }

  closeDeleteDialog(): void {
    this.deleteClient = undefined;
  }

  closeModal(): void {
    this.formGroup = undefined;
  }

  ngOnDestroy(): void {
    this.subscription.forEach((x) => {
      x.unsubscribe();
    });
  }
}
