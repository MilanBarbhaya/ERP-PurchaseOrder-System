import { TestBed } from '@angular/core/testing';
import {
  HttpClientTestingModule,
  HttpTestingController
} from '@angular/common/http/testing';

import { PurchaseOrderService } from './purchase-order-service';

describe(
  'PurchaseOrderService',
  () => {

    let service:
      PurchaseOrderService;

    let httpMock:
      HttpTestingController;

    beforeEach(() => {

      TestBed.configureTestingModule({

        imports: [
          HttpClientTestingModule
        ]
      });

      service =
        TestBed.inject(
          PurchaseOrderService
        );

      httpMock =
        TestBed.inject(
          HttpTestingController
        );
    });

    it(
      'should get purchase orders',
      () => {

        const mockData = [
          {
            id: '1',
            vendorName: 'Dell'
          }
        ];

        service.getAll()
          .subscribe(data => {

            expect(
              data.length
            ).toBe(1);
          });

        const req =
          httpMock.expectOne(
            'http://localhost:5237/api/purchase-orders'
          );

        expect(
          req.request.method
        ).toBe('GET');

        req.flush(mockData);
      });
  });
