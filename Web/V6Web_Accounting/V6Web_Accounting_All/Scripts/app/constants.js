!function (win) {
    win.Const = Object.freeze({
        AppModule: 'app',
        BreezeEnManager: 'brzMgr',
        BreezeDataService: 'brzDataSvc',
        DataServiceFactory: 'dataSvcFac',
        HttpDataService: 'httpDataSvc',
        LoggerService: 'logSvc'
    });

    win.Entity = Object.freeze({
        Customer: 'Customer',
        Receipt: 'Receipt',
        ReceiptDetail: 'ReceiptDetail',
        Material: 'Material'
    });
}(window);