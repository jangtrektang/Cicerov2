import { CiceroAngularPage } from './app.po';

describe('cicero-angular App', function() {
  let page: CiceroAngularPage;

  beforeEach(() => {
    page = new CiceroAngularPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
