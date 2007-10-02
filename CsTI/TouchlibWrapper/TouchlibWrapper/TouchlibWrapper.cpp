//
//implementation file for TouchlibWrapper
//most of the code taken from the OSC demo app included in the touchlib source
//

//i have no idea what stdafx is and why it should be here
//something of precompiled headers...i'm not a vc++ master :-)
#include "stdafx.h"
#include "TouchlibWrapper.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


CTouchlibWrapper::CTouchlibWrapper()
{
}

CTouchlibWrapper::~CTouchlibWrapper()
{
	TouchScreenDevice::destroy();
}


CTouchlibWrapper::CTouchlibWrapper(CFeedback* pFeedback)
{

	m_pFeedback =  pFeedback;
	
	char * config = "config.xml";
		
	screen = TouchScreenDevice::getTouchScreen();
	//screen->setDebugMode(false);

	if(!screen->loadConfig("config.xml"))
	{
		std::string label;
		label = screen->pushFilter("dsvlcapture");
		screen->setParameter(label, "source", "cam");
		screen->pushFilter("mono");
		screen->pushFilter("smooth");
		screen->pushFilter("backgroundremove");

		label = screen->pushFilter("brightnesscontrast");
		screen->setParameter(label, "brightness", "0.1");
		screen->setParameter(label, "contrast", "0.4");
		label = screen->pushFilter("rectify");

		screen->setParameter(label, "level", "25");
		
		

		screen->saveConfig("config.xml");
	}

	std::string bgLabel = screen->findFirstFilter("backgroundremove");
	std::string recLabel = screen->findFirstFilter("rectify");

	//set to 'this' so the dll gets the touch events
	screen->registerListener((ITouchListener *)this);
	// Note: Begin processing should only be called after the screen is set up

	SLEEP(1000);
	screen->setParameter(bgLabel, "mask", (char*)screen->getCameraPoints());
	screen->setParameter(bgLabel, "capture", "");

	screen->beginProcessing();
	screen->beginTracking();
	do
	{

		int keypressed = cvWaitKey(32) & 255;

		if(keypressed != 255 && keypressed > 0)
			fprintf(stderr,"KP: %d\n", keypressed);
        if( keypressed == 27) break;		// ESC = quit
        if( keypressed == 98)				// b = recapture background
		{
			screen->setParameter(((std::string)"background4"), "capture", "");
			
		}
        if( keypressed == 114)				// r = auto rectify..
		{
			screen->setParameter(((std::string)"rectify6"), "level", "auto");
		}

		screen->getEvents();



	} while( true );
	
	
	std::cout << "SCREEN SHUTTING DOWN" << std::endl;
}


void CTouchlibWrapper::fingerDown(TouchData data)
{
	m_pFeedback->OnFingerDown( data );
}


void CTouchlibWrapper::fingerUpdate(TouchData data)
{
	m_pFeedback->OnFingerUpdate( data );
}

void CTouchlibWrapper::fingerUp(TouchData data)
{
	m_pFeedback->OnFingerUp( data );
}

