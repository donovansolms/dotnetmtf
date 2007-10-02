/*
	This is the main meat of the TouchlibWrapper. It interacts with touchlib
	and had the overrides for the finger events
*/
#define WIN32_LEAN_AND_MEAN
#include <windows.h>
#include <stdlib.h>
#include <malloc.h>
#include <memory.h>
#include <tchar.h>
#include <math.h>


#include <cv.h>
#include <cxcore.h>
#include <highgui.h>

#include "TouchScreenDevice.h"

#include "resource.h"

using namespace touchlib;

#include <stdio.h>
#include <string>


#pragma once

//allows the managed part to access methods
#ifdef UNMANAGED_EXPORTS
#define UNMANAGED_API __declspec(dllexport)
#else
#define UNMANAGED_API __declspec(dllimport)
#endif

class UNMANAGED_API CTouchlibWrapper : public ITouchListener
{
public:
	//because we like a constructor
	CTouchlibWrapper();
	//because we like a destructor
	~CTouchlibWrapper();

	//class that sned to CsTI.dll
	class UNMANAGED_API CFeedback
	{
	public:
		virtual void OnFingerDown( TouchData data ) = 0;
		virtual void OnFingerUpdate( TouchData data ) = 0;
		virtual void OnFingerUp( TouchData data ) = 0;
	};

	//Constructor to allow the sending of touch events
	CTouchlibWrapper(CFeedback* pFeedback);

	//overrides needed
	void fingerDown( TouchData data );
	void fingerUp( TouchData data );
	void fingerUpdate( TouchData data );

private:
	CFeedback* m_pFeedback;
	ITouchScreen *screen;
};

